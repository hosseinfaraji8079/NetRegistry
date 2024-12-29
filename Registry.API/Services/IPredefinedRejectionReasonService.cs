using Registry.API.ViewModel;

namespace Registry.API.Services;

/// <summary>
/// Defines methods for managing predefined rejection reasons.
/// </summary>
public interface IPredefinedRejectionReasonService
{
    /// <summary>
    /// Retrieves all predefined rejection reasons.
    /// </summary>
    /// <returns>A list of predefined rejection reason DTOs.</returns>
    Task<List<PredefinedRejectionReasonDto>> GetAsync();
}