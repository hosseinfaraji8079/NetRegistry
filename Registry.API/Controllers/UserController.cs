using System.Net;
using Microsoft.AspNetCore.Mvc;
using Registry.API.Extensions;
using Registry.API.Filters;
using Registry.API.Services;
using Registry.API.ViewModel;

namespace Registry.API.Controllers;

public class UserController(IUserService userService) : DefaultController 
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult),(int)HttpStatusCode.OK)]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResult<string>),(int)HttpStatusCode.OK)]
    public async Task<ApiResult<string>> Post([FromBody] AddUserDto user)
    {
        return Ok(await userService.GetTokenUserAsync(user));
    }
}