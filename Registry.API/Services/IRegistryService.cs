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
    /// <param name="userId">The filed for get user registry items</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the filtered registry results as a <see cref="FilterRegistryDto"/>.</returns>
    Task<FilterRegistryDto> FilterAsync(FilterRegistryDto filter, long? userId);

    /// <summary>
    /// Adds a new registry entry to the system.
    /// </summary>
    /// <param name="registry">The registry data to add, provided as an <see cref="AddRegistryDto"/> object.</param>
    /// <param name="userId">The registry for who</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddAsync(AddRegistryDto registry,long userId);
    
    /// <summary>
    /// Processes a registry entry by accepting or rejecting it based on the provided data.
    /// </summary>
    /// <param name="decisionDto">The data to process the registry entry.</param>
    /// <returns>An indicating the outcome of the operation.</returns>
    Task ProcessRegistryDecisionAsync(RegistryDecisionDto decisionDto);

    Task SendPriceAndLink(SendPriceAndLinkForPaymentDto accept);
}