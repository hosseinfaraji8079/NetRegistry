using System.Net;
using Microsoft.AspNetCore.Mvc;
using Registry.API.Extensions;
using Registry.API.Filters;
using Registry.API.Services;

namespace Registry.API.Controllers;

/// <summary>
/// Controller for managing user authorization and permissions.
/// Initializes a new instance of the <see cref="AuthorizationController"/> class.
/// </summary>
public class AuthorizationController(IAuthorizationService service) : DefaultController
{
    /// <summary>
    /// Retrieves the list of all permissions assigned to the current user.
    /// </summary>
    /// <returns>
    /// An <see cref="ApiResult{List{string}}"/> containing the list of permission names.
    /// </returns>
    [HttpGet("permissions")]
    [ProducesResponseType(typeof(List<string>),(int)HttpStatusCode.OK)]
    [ProducesDefaultResponseType]
    public async Task<ApiResult<List<string>>> GetPermissions()
    {
        return Ok(await service.GetUserPermissions(User.GetId()));
    }
    
    /// <summary>
    /// Checks whether the current user has a specific permission.
    /// </summary>
    /// <param name="permissionName">The name of the permission to check.</param>
    /// <returns>
    /// An <see cref="ApiResult{bool}"/> indicating whether the user has the specified permission.
    /// </returns>
    [HttpGet("has-permission/{permissionName}")]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> HasUserPermission(string permissionName)
    {
        // Retrieve the current user's ID from the claims
        long userId = User.GetId();

        // Check if the user has the specified permission
        bool hasPermission = await service.HasUserPermission(userId, permissionName);

        // Return the result wrapped in an ApiResult
        return Ok(hasPermission);
    }
}