using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Registry.API.Extensions;
using Registry.API.Services;
using Registry.API.ViewModel;

namespace Registry.API.Hubs;

public class SupporterOnlineHub(IUserService userService,IAuthorizationService authorizationService) : Hub
{
    private static List<UserDto> Supporters = new ();
    
    public override async Task OnConnectedAsync()
    {
        long userId = Context.User.GetId();
        
        bool isSupporter = await authorizationService.HasUserPermission(userId, "supporter");
        
        if (isSupporter)
        {
            UserDto supporter =  await userService.GetUserByIdAsync(userId);
            Supporters.Add(supporter);
            
            await Clients.All.SendAsync("UpdateSupporterOnline", Supporters);
        }
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        long userId = Context.User.GetId();
        Supporters.RemoveAll(x=>x.Id == userId);
        await Clients.All.SendAsync("UpdateSupporterOnline", Supporters);
        await base.OnDisconnectedAsync(exception);
    }
}