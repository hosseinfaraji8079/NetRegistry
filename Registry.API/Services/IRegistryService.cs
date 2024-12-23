using Registry.API.ViewModel;

namespace Registry.API.Services;

/// <summary>
/// Service interface for managing registry-related operations.
/// Provides methods for filtering and adding registry entries.
/// </summary>
public interface IRegistryService
{
    /// <summary>
    /// Filters the registry entries based on the specified criteria.
    /// </summary>
    /// <param name="filter">The filtering criteria provided as a <see cref="FilterRegistryDto"/> object.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the filtered registry results as a <see cref="FilterRegistryDto"/>.</returns>
    Task<FilterRegistryDto> FilterAsync(FilterRegistryDto filter);

    /// <summary>
    /// Adds a new registry entry to the system.
    /// </summary>
    /// <param name="registry">The registry data to add, provided as an <see cref="AddRegistryDto"/> object.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddAsync(AddRegistryDto registry);

    /// <summary>
    /// Accepts a registry request by processing the provided data.
    /// This method is typically used to approve or process pending registry entries.
    /// </summary>
    /// <param name="accept">The data required to accept the registry request, including necessary identifiers and metadata.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AcceptRegistryAsync(AcceptRegistryDto accept);
}