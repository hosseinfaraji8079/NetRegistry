using Microsoft.AspNetCore.Mvc;
using Registry.API.Extensions;
using Registry.API.Services;
using Registry.API.ViewModel;

namespace Registry.API.Controllers;

public class UserController(IUserService userService) : DefaultController 
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(User.GetId());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddUserDto user)
    {
        return Ok(await userService.RegisterUserAsync(user));
    }
}