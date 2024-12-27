using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Registry.API.Extensions;
using Registry.API.Models;
using System.Collections.Concurrent;

namespace Registry.API.Hubs;

public class UsersHubs : Hub
{
    private static ConcurrentDictionary<long, bool> OnlineUsers = new ();

    public override Task OnConnectedAsync()
    {
        long userId = Context.User.GetId();
        OnlineUsers.TryAdd(userId, true);
        Clients.All.SendAsync("UpdateOnlineCount", OnlineUsers.Count);
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        long userId = Context.User.GetId();
        OnlineUsers.TryRemove(userId, out _);
        await Clients.All.SendAsync("UpdateOnlineCount", OnlineUsers.Count);
        await base.OnDisconnectedAsync(exception);
    }
    
    public Task<int> GetOnlineCountAsync()
    {
        return Task.FromResult(OnlineUsers.Count);
    }
}