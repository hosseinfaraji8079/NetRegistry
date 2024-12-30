using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Registry.API.Enums;
using Registry.API.Extensions;
using Registry.API.ViewModel;

namespace Registry.API.Hubs;

/// <summary>
/// Represents a SignalR hub for managing registry payments.
/// </summary>
// [Authorize]
public class PaymentHubs(ILogger<PaymentHubs> logger) : Hub
{
    /// <summary>
    /// A thread-safe dictionary that holds all pending registries.
    /// Key: RegistryId, Value: RegistryDto
    /// </summary>
    private static ConcurrentDictionary<long, RegistryDto> _pendingRegistries = new();
    
    /// <summary>
    /// Handles the event when a client disconnects from the hub.
    /// If the client has any pending registries, their prices are set to 0 (canceled).
    /// </summary>
    /// <param name="exception">The exception that caused the disconnection, if any.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        long userId = Context.User.GetId();

        // Find all pending registries for the disconnected user
        var userPendingRegistries = _pendingRegistries
            .Where(x => x.Value.UserId == userId)
            .Select(x => x.Key)
            .ToList();

        foreach (var registryId in userPendingRegistries)
        {
            if (_pendingRegistries.TryGetValue(registryId, out var registry))
            {
                // Set Price to 0 to indicate cancellation
                registry.Price = 0;
                registry.Status = RegistryStatus.Rejected;

                logger.LogInformation(
                    "Registry {RegistryId} for User {UserId} has been cancelled due to disconnection.", registryId,
                    userId);

                // Notify all clients about the updated registry
                await Clients.All.SendAsync("PaymentUpdated", registry);

                // Remove from pending registries
                _pendingRegistries.TryRemove(registryId, out _);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Registers a new payment by adding it to the pending registries list.
    /// The registry remains in a loading state until confirmed by support.
    /// </summary>
    /// <param name="registryDto">The registry DTO to be added.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task RegisterPayment(RegistryDto registryDto)
    {
        long userId = Context.User.GetId();

        // Assign the user ID to the registry
        registryDto.UserId = userId;

        // Set initial status to PendingReview
        registryDto.Status = RegistryStatus.AwaitingPayment;

        // Assign a unique Id if not set (ensure unique generation based on your requirements)
        if (registryDto.Id == 0)
        {
            registryDto.Id = Guid.NewGuid().GetHashCode(); // Simplistic unique ID generation
            // Consider using a more robust unique identifier strategy
        }

        // Add to pending registries
        bool added = _pendingRegistries.TryAdd(registryDto.Id, registryDto);

        if (added)
        {
            logger.LogInformation("User {UserId} registered a new payment with Registry ID {RegistryId}.", userId,
                registryDto.Id);

            // Notify all clients about the new pending registry
            await Clients.All.SendAsync("PaymentRegistered", registryDto);
        }
        else
        {
            logger.LogWarning("Failed to register payment for User {UserId} with Registry ID {RegistryId}.", userId,
                registryDto.Id);
            await Clients.Caller.SendAsync("Error", "Failed to register payment. Please try again.");
        }
    }

    /// <summary>
    /// Confirms a pending payment by setting its price and payment link.
    /// This method should be invoked by the support team.
    /// </summary>
    /// <param name="registryId">The unique identifier of the registry to confirm.</param>
    /// <param name="price">The confirmed price for the registry.</param>
    /// <param name="paymentLink">The payment link associated with the registry.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task ConfirmPayment(long registryId, long price, string paymentLink)
    {
        if (_pendingRegistries.TryGetValue(registryId, out var registry))
        {
            // Update the registry with price and payment link
            registry.Price = price;
            registry.PaymentLink = paymentLink;
            registry.Status = RegistryStatus.Rejected;

            logger.LogInformation(
                "Registry {RegistryId} has been confirmed with Price {Price} and PaymentLink {PaymentLink}.",
                registryId, price, paymentLink);

            // Notify all clients about the updated registry
            await Clients.All.SendAsync("PaymentUpdated", registry);

            // Remove from pending registries as it's now confirmed
            _pendingRegistries.TryRemove(registryId, out _);
        }
        else
        {
            logger.LogWarning("Attempted to confirm non-existent or already processed Registry ID {RegistryId}.", registryId);
            await Clients.Caller.SendAsync("Error", $"Registry with ID {registryId} not found or already processed.");
        }
    }

    /// <summary>
    /// Cancels a pending payment by setting its price to 0.
    /// This can be invoked manually if needed.
    /// </summary>
    /// <param name="registryId">The unique identifier of the registry to cancel.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task CancelPayment(long registryId)
    {
        if (_pendingRegistries.TryGetValue(registryId, out var registry))
        {
            // Set Price to 0 to indicate cancellation
            registry.Price = 0;
            registry.Status = RegistryStatus.Rejected;

            logger.LogInformation("Registry {RegistryId} has been cancelled manually.", registryId);

            // Notify all clients about the updated registry
            await Clients.All.SendAsync("PaymentUpdated", registry);

            // Remove from pending registries
            _pendingRegistries.TryRemove(registryId, out _);
        }
        else
        {
            logger.LogWarning("Attempted to cancel non-existent or already processed Registry ID {RegistryId}.",
                registryId);
            await Clients.Caller.SendAsync("Error", $"Registry with ID {registryId} not found or already processed.");
        }
    }
}
