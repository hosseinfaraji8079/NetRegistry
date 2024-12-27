using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using Registry.API.Enums;
using Registry.API.Repositories.Interfaces;
using Registry.API.ViewModel;

namespace Registry.API.Services;

public class RegistryService(IRegistryRepository repository,IMapper mapper) : IRegistryService
{
    public async Task<FilterRegistryDto> FilterAsync(FilterRegistryDto filter,long? userId)
    {
        Expression<Func<Models.Registry, bool>> predicate = x => true;

        if (userId is not null) predicate = x => x.UserId == userId;
        
        var data = await repository.GetAsync(predicate,true);
        
        var mappedData = mapper.Map<IEnumerable<RegistryDto>>(data);
        
        await filter.Paging(mappedData);
        return filter;
    }

    public async Task AddAsync(AddRegistryDto registry,long userId)
    {
        var main = mapper.Map<Models.Registry>(registry);
        main.UserId = userId;
        
        if (await repository.ExistsAsync(x => x.ImeI_1 == registry.ImeI_1 && x.ImeI_2 == registry.ImeI_2))
            throw new ValidationException("شماره IMEI وارد شده قبلاً ثبت شده است.");
        
        await repository.AddAsync(main);
    }

    public async Task AcceptRegistryAsync(AcceptRegistryDto accept)
    {
        var registry = await repository.GetByIdAsync(accept.Id);

        if (registry is null) throw new ApplicationException($"not found registry by id {accept.Id}");

        registry.Status = RegistryStatus.AwaitingPayment;
        registry.Model = accept.Model;
        
        await repository.UpdateAsync(registry);
    }
}