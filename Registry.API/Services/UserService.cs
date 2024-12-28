using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Registry.API.Helpers;
using Registry.API.Models;
using Registry.API.Repositories.Interfaces;
using Registry.API.ViewModel;

namespace Registry.API.Services;

public class UserService(IAsyncRepository<User> userRepository, IMapper mapper, ILogger<UserService> logger)
    : IUserService
{
    public async Task<string> GetTokenUserAsync(AddUserDto user)
    {
        logger.LogInformation("generate token started for user by chat id {chatId}", user.ChatId);

        var mainUser = mapper.Map<User>(user);
        mainUser.Password = "HOSSEIN*(!^";
        var existUser = await userRepository.GetQueryableAsync().SingleOrDefaultAsync(x => x.ChatId == user.ChatId);

        logger.LogInformation("generate token started for user by chat id {chatId} finished", user.ChatId);

        return existUser switch
        {
            null => JwtHelper.GenerateToken(await userRepository.AddAsync(mainUser)),
            _ => JwtHelper.GenerateToken(existUser)
        };
    }

    public async Task<UserDto> GetUserByIdAsync(long userId)
    {
        logger.LogInformation($"get user by id {userId}");
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null) throw new ApplicationException($"not found user by id : {userId}");
        logger.LogInformation("mapping user");
        logger.LogInformation($"get user by id {userId} finished");
        return mapper.Map<UserDto>(user);
    }
}