using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Registry.API.Extensions;
using Registry.API.Filters;
using Registry.API.Hubs;
using Registry.API.Services;
using Registry.API.ViewModel;

namespace Registry.API.Controllers;

public class UserController(IUserService userService) : DefaultController 
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<List<long>>),(int)HttpStatusCode.OK)]
    public ApiResult<List<long>> Get()
    {
        var s = HttpContext.User;
        var d = User;
        return Ok(1);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResult<string>),(int)HttpStatusCode.OK)]
    public async Task<ApiResult<string>> Post([FromBody] AddUserDto user)
    {
        return Ok(await userService.GetTokenUserAsync(user));
    }
}