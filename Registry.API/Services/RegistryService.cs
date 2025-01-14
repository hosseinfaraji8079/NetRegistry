﻿using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Registry.API.Enums;
using Registry.API.Filters;
using Registry.API.Models;
using Registry.API.Repositories.Interfaces;
using Registry.API.ViewModel;

namespace Registry.API.Services;

public class RegistryService(
    IRegistryRepository repository,
    IUserService userService,
    IMapper mapper,
    ILogger<RegistryService> logger,
    IAsyncRepository<RejectionReason> rejectionRepository,
    IAsyncRepository<PredefinedRejectionReason> predefinedRepository) : IRegistryService
{
    public async Task<FilterRegistryDto> FilterAsync(FilterRegistryDto filter, long? userId)
    {
        Expression<Func<Models.Registry, bool>> predicate = x =>
            (userId == null || x.UserId == userId) &&
            (filter.Status == null || x.Status == filter.Status) &&
            (string.IsNullOrEmpty(filter.Imei) || EF.Functions.Like(x.ImeI_1, $"%{filter.Imei}%"));

        var data = await repository.GetAsync(
            predicate: predicate,
            includeString: "RejectionReasons.PredefinedRejectionReason"
        );

        var mappedData = mapper.Map<IEnumerable<RegistryDto>>(data);

        await filter.Paging(mappedData);
        return filter;
    }

    public async Task UpdateAsync(UpdateRegistryDto registry, long userId)
    {
        var existingEntity = await repository.GetByIdAsync(registry.Id);

        if (existingEntity is null || existingEntity.UserId != userId) throw new ApplicationException("not found");

        mapper.Map(registry, existingEntity);
        existingEntity.Status = RegistryStatus.PendingReview;

        await repository.UpdateAsync(existingEntity);
    }

    public async Task AddAsync(AddRegistryDto registry, long userId)
    {
        var main = mapper.Map<Models.Registry>(registry);
        main.UserId = userId;
        main.UniqueId = Guid.NewGuid().ToString("N");

        if (await repository.ExistsAsync(x =>
                (x.ImeI_1 == registry.ImeI_1 && x.ImeI_2 == registry.ImeI_2) && x.Status != RegistryStatus.Rejected))
            throw new ValidationException("شماره IMEI وارد شده قبلاً ثبت شده است.");

        await repository.AddAsync(main);
    }

    public async Task ProcessRegistryDecisionAsync(RegistryDecisionDto decisionDto)
    {
        logger.LogInformation("Processing registry entry with ID: {RegistryId}", decisionDto.Id);

        var registry = await repository.GetByIdAsync(decisionDto.Id);

        if (registry == null)
        {
            logger.LogWarning("Registry entry with ID {RegistryId} not found.", decisionDto.Id);
            throw new ApplicationException("Registry not found.");
        }

        if (!string.IsNullOrWhiteSpace(decisionDto.Model))
        {
            registry.Model = decisionDto.Model;
            registry.Status = RegistryStatus.AwaitingPayment;

            await repository.UpdateAsync(registry);
            logger.LogInformation("Registry entry with ID {RegistryId} accepted successfully.", decisionDto.Id);
        }
        else
        {
            // Reject the registry
            if (!decisionDto.PredefinedRejectionReasonId.HasValue)
            {
                logger.LogWarning("Rejection reason ID not provided for registry ID {RegistryId}.", decisionDto.Id);
                throw new ApplicationException("Predefined rejection reason ID is required.");
            }

            var rejectionReason = await predefinedRepository.GetByIdAsync(decisionDto.PredefinedRejectionReasonId ?? 0);

            if (rejectionReason == null)
            {
                logger.LogWarning("Predefined rejection reason with ID {ReasonId} not found.",
                    decisionDto.PredefinedRejectionReasonId.Value);
                throw new ApplicationException("Invalid rejection reason ID.");
            }

            registry.Status = RegistryStatus.Rejected;

            var rejection = new RejectionReason
            {
                RegistryId = registry.Id,
                PredefinedRejectionReasonId = rejectionReason.Id,
                AdditionalExplanation = decisionDto.AdditionalExplanation
            };

            await rejectionRepository.AddAsync(rejection);
            await repository.UpdateAsync(registry);

            logger.LogInformation("Registry entry with ID {RegistryId} rejected successfully.", decisionDto.Id);
        }
    }

    public Task SendPriceAndLink(SendPriceAndLinkForPaymentDto accept)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> GetUniqueIdAsync(long id)
    {
        var registry = await repository.GetByIdAsync(id);
        return registry switch
        {
            null => throw new ApplicationException($"not found unique by id {id}"),
            _ => registry.UniqueId,
        };
    }

    public async Task AcceptedPayment(string unique)
    {
        var registry = await repository
            .GetQueryableAsync()
            .SingleOrDefaultAsync(x => x.UniqueId == unique);
        
        if (registry is null || registry.Status != RegistryStatus.AwaitingPayment)
            throw new ApplicationException("not exist");
            
        registry.UniqueId = Guid.NewGuid().ToString("N");
        registry.Status = RegistryStatus.QueuedForOperation;
        
        await repository.UpdateAsync(registry);
        var user = await userService.GetUserParentAsync(registry.UserId);
        // todo : increase user balance 
    }

    public async Task RejectPayment(string unique)
    {
        var registry = await repository.GetQueryableAsync().SingleOrDefaultAsync(x => x.UniqueId == unique);

        if (registry is null || registry.Status != RegistryStatus.AwaitingPayment)
            throw new ApplicationException("not exist");

        registry.UniqueId = Guid.NewGuid().ToString("N");
        registry.Status = RegistryStatus.Rejected;

        await repository.UpdateAsync(registry);
    }

    public async Task<RegistryDto> GetRegistryById(long id, long? userId)
    {
        var registry = await repository.GetByIdAsync(
            id: id,
            includeString: "RejectionReasons.PredefinedRejectionReason");

        if (registry is null || (userId is not null && registry.UserId != userId))
            throw new ApplicationException($"not exists by Id {id}");

        return mapper.Map<RegistryDto>(registry);
    }

    public async Task UpdateCustomInformation(UpdateRegistryCustomsDto updateRegistry)
    {
        var main = await repository.GetByIdAsync(updateRegistry.Id);
        if (main is null) throw new ApplicationException($"not found by Id {updateRegistry.Id}");

        mapper.Map(updateRegistry, main);
        main.Status = RegistryStatus.Finished;

        await repository.UpdateAsync(main);
    }
}