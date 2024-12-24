using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Registry.API.Helpers;
using Registry.API.Models;
using Registry.API.Repositories.Interfaces;
using Registry.API.ViewModel;

namespace Registry.API.Services;

public class UserService(IAsyncRepository<User> userRepository, IMapper mapper) : IUserService
{
    public async Task<string> GetTokenUserAsync(AddUserDto user)
    {
        var mainUser = mapper.Map<User>(user);
        mainUser.Password = "HOSSEIN*(!^";
        var existUser = await userRepository.GetQueryableAsync().SingleOrDefaultAsync(x => x.ChatId == user.ChatId);

        return existUser switch
        {
            null => JwtHelper.GenerateToken(await userRepository.AddAsync(mainUser)),
            _ => JwtHelper.GenerateToken(existUser)
        };
    }
}