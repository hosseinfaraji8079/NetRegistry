using AutoMapper;
using Registry.API.Models;
using Registry.API.Repositories.Interfaces;
using Registry.API.ViewModel;

namespace Registry.API.Services;

public class UserService(IAsyncRepository<User> userRepository,IMapper mapper) : IUserService
{
    public async Task<string> RegisterUserAsync(AddUserDto user)
    {
        var mainUser = mapper.Map<User>(user);
        await userRepository.AddAsync(mainUser);
        
        //generate token and send
        
        return "";
    }
}