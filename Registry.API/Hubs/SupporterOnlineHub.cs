using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Registry.API.Extensions;
using Registry.API.Services;
using Registry.API.ViewModel;

namespace Registry.API.Hubs
{
    /// <summary>
    /// A SignalR hub that tracks which supporters are currently online
    /// and notifies clients about online supporter updates.
    /// </summary>
    public class SupporterOnlineHub(IUserService userService, IAuthorizationService authorizationService) : Hub
    {
        /// <summary>
        /// A static list of currently online supporters.
        /// Synchronized via a lock to ensure thread safety.
        /// </summary>
        private static readonly object _syncLock = new();
        private static readonly List<UserDto> _supporters = new();

        /// <summary>
        /// Called when a client connects. 
        /// If the user has a "supporter" permission, they are added to the online supporters list.
        /// All clients are notified of the updated supporter list.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task OnConnectedAsync()
        {
            long userId = Context.User.GetId();

            bool isSupporter = await authorizationService.HasUserPermission(userId, "supporter");

            if (isSupporter)
            {
                // Retrieve user info
                UserDto supporter = await userService.GetUserByIdAsync(userId);

                // Lock to prevent concurrent modifications to the list
                lock (_syncLock)
                {
                    // Only add if this supporter is not already in the list
                    bool alreadyInList = _supporters.Exists(x => x.Id == userId);
                    if (!alreadyInList)
                    {
                        _supporters.Add(supporter);
                    }
                }

                // Notify all clients about the updated list
                await Clients.All.SendAsync("UpdateSupporterOnline", _supporters);
            }

            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Called when a client disconnects. 
        /// Removes the user from the supporter list if they are present, 
        /// then notifies all clients of the updated list.
        /// </summary>
        /// <param name="exception">An optional exception if the disconnection was caused by an error.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            long userId = Context.User.GetId();

            lock (_syncLock)
            {
                _supporters.RemoveAll(x => x.Id == userId);
            }

            // Notify all clients about the updated list
            await Clients.All.SendAsync("UpdateSupporterOnline", _supporters);

            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Returns the current list of online supporters.
        /// </summary>
        /// <returns>A list of <see cref="UserDto"/> representing online supporters.</returns>
        public Task<List<UserDto>> GetOnlineSupporterAsync()
        {
            // Return a copy to avoid exposing the internal list
            lock (_syncLock)
            {
                return Task.FromResult(_supporters.ToList());
            }
        }
    }
}
