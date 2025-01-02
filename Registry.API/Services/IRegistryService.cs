using Registry.API.Filters;
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

    /// <summary>
    /// Sends the price and payment link to the user based on the provided details.
    /// </summary>
    /// <param name="accept">An instance of <see cref="SendPriceAndLinkForPaymentDto"/> containing the required data to generate and send the payment link.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation to send the price and link.</returns>
    Task SendPriceAndLink(SendPriceAndLinkForPaymentDto accept);

    /// <summary>
    /// Retrieves a unique identifier (UID) for a specific operation or transaction based on the provided ID.
    /// </summary>
    /// <param name="id">The unique identifier associated with the request.</param>
    /// <returns>A <see cref="Task{TResult}"/> containing an <see cref="ApiResult{T}"/> with the generated unique identifier as a string.</returns>
    Task<string?> GetUniqueIdAsync(long id);
    
    /// <summary>
    /// Marks a payment as accepted based on the provided unique identifier.
    /// </summary>
    /// <param name="unique">The unique identifier associated with the payment.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AcceptedPayment(string unique);

    /// <summary>
    /// Marks a payment as rejected based on the provided unique identifier.
    /// </summary>
    /// <param name="unique">The unique identifier associated with the payment.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task RejectPayment(string unique);
}