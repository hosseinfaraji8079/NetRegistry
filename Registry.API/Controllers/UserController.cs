using Microsoft.AspNetCore.Mvc;

namespace Registry.API.Controllers;

public class UserController : DefaultController 
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello");
    }
}