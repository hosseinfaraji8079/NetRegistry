using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Registry.API.Extensions;
using Registry.API.Filters;
using Registry.API.Hubs;
using Registry.API.Services;
using Registry.API.ViewModel;
using IAuthorizationService = Registry.API.Services.IAuthorizationService;

namespace Registry.API.Controllers;

public class UserController(IUserService userService,IAuthorizationService authorizationService) : DefaultController 
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<List<long>>),(int)HttpStatusCode.OK)]
    public async Task<ApiResult<List<long>>> Get()
    {
        var s = HttpContext.User;
       var d = User;
       var dd = await authorizationService.HasUserPermission(1, "supporter");
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