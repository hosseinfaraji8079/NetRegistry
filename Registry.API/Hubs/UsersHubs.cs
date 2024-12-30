using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Registry.API.Extensions;

namespace Registry.API.Hubs
{
    /// <summary>
    /// A SignalR hub to manage user connections and track how many users are online.
    /// </summary>
    public class UsersHubs : Hub
    {
        /// <summary>
        /// A thread-safe dictionary storing the online status of connected users.
        /// The key is the user ID, and the value indicates the user is online.
        /// </summary>
        private static readonly ConcurrentDictionary<long, bool> _onlineUsers = new();

        /// <summary>
        /// Called when a new connection is established. Attempts to add the user to the online list
        /// and notifies all clients of the updated count if successful.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task OnConnectedAsync()
        {
            long userId = Context.User.GetId();

            // TryAdd returns true if this userId was not present and was successfully added
            bool added = _onlineUsers.TryAdd(userId, true);

            if (added)
            {
                // Only notify if the user is newly added
                await Clients.All.SendAsync("UpdateOnlineCount", _onlineUsers.Count);
            }

            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Called when a connection is terminated. Removes the user from the online list
        /// and notifies all clients of the updated count.
        /// </summary>
        /// <param name="exception">The exception (if any) that caused the disconnection.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            long userId = Context.User.GetId();

            _onlineUsers.TryRemove(userId, out _);

            await Clients.All.SendAsync("UpdateOnlineCount", _onlineUsers.Count);

            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Retrieves the current number of online users.
        /// </summary>
        /// <returns>An integer representing the current online users count.</returns>
        public Task<int> GetOnlineCountAsync()
        {
            return Task.FromResult(_onlineUsers.Count);
        }
    }
}
