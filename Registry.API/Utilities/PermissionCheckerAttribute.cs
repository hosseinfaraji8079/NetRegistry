using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Registry.API.Enums;
using Registry.API.Extensions;
using Registry.API.Filters;
using IAuthorizationService = Registry.API.Services.IAuthorizationService;

namespace Registry.API.Utilities
{
    public class PermissionCheckerAttribute(string permission) : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private IAuthorizationService? _authorizeService;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity!.IsAuthenticated)
            {
                long userId = context.HttpContext.User.GetId();
                
                _authorizeService =
                    (IAuthorizationService)context.HttpContext.RequestServices.GetService(typeof(IAuthorizationService))!;
                
                if (! await _authorizeService.HasUserPermission(userId, permission))
                {
                    context.Result = new JsonResult(new ApiResult(false,ApiResultStatusCode.LogicError,"شما به این بخش دسترسی ندارید"));
                }
            }
        }
    }
}