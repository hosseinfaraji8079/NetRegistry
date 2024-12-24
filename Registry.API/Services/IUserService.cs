using Registry.API.Models;
using Registry.API.ViewModel;

namespace Registry.API.Services;

public interface IUserService
{
    Task<string> RegisterUserAsync(AddUserDto user);
}