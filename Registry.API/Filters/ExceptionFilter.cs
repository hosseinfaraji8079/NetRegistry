using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Registry.API.Enums;

namespace Registry.API.Filters;

public class ExceptionFilter : IAsyncExceptionFilter
{
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        string? controller = context.RouteData.Values["controller"]?.ToString();
        string? action = context.RouteData.Values["action"]?.ToString();
        string? version = context.RouteData.Values["v"]?.ToString();
        context.Result =
            new JsonResult(new ApiResult(false, ApiResultStatusCode.LogicError, context.Exception.Message));
        await Task.CompletedTask;
    }
}