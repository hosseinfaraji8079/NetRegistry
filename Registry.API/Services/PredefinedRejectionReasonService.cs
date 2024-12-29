using AutoMapper;
using Registry.API.Models;
using Registry.API.Repositories.Interfaces;
using Registry.API.ViewModel;

namespace Registry.API.Services;

public class PredefinedRejectionReasonService(
    IAsyncRepository<PredefinedRejectionReason> repository,
    ILogger<PredefinedRejectionReasonService> logger,
    IMapper mapper) : IPredefinedRejectionReasonService
{
    public async Task<List<PredefinedRejectionReasonDto>> GetAsync()
    {
        logger.LogInformation("Starting retrieval of predefined rejection reasons.");
        try
        {
            var predefinedRejectionReasons = await repository.GetAllAsync();
            var dtoList = mapper.Map<List<PredefinedRejectionReasonDto>>(predefinedRejectionReasons);

            logger.LogInformation("Successfully retrieved {Count} predefined rejection reasons.", dtoList.Count);
            return dtoList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving predefined rejection reasons.");
            throw;
        }
    }
}