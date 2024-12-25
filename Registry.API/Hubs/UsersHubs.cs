using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Registry.API.Extensions;
using Registry.API.Models;

namespace Registry.API.Hubs;

public class UsersHubs : Hub
{
    private static List<long> OnlineUser = new();

    public override Task OnConnectedAsync()
    {
        long userId = Context.User.GetId();
        OnlineUser.Add(userId);
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        long userId = Context.User.GetId();
        OnlineUser.Remove(userId);
        await base.OnDisconnectedAsync(exception);
    }

    public List<long> UserIdAsync()
    {
        return OnlineUser;
    }
}