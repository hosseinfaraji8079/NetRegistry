using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Registry.API.Filters;

namespace Registry.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiResultFilter]
[ServiceFilter(typeof(ExceptionFilter))]
[Authorize]
public class DefaultController : ControllerBase 
{
    
}