namespace Registry.API.Services;

/// <summary>
/// Defines methods for managing and verifying user permissions within the application.
/// </summary>
public interface IAuthorizationService
{
    /// <summary>
    /// Checks whether a specific user has the specified permission.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="permissionName">The name of the permission to check.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains <c>true</c> if the user has the specified permission; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> HasUserPermission(long userId, string permissionName);
    
    /// <summary>
    /// Retrieves the list of all permissions assigned to a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a list of permission names assigned to the user.
    /// </returns>
    Task<List<string>> GetUserPermissions(long userId);
}