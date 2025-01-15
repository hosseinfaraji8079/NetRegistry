using Registry.API.ViewModel;

namespace Registry.API.Services;

/// <summary>
/// Provides methods to manage user-related operations, such as retrieving user details and generating tokens.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Generates a token for the specified user.
    /// </summary>
    /// <param name="user">The user details required for token generation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the generated token as a string.</returns>
    Task<string> GetTokenUserAsync(AddUserDto user);

    /// <summary>
    /// Retrieves a user's details by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the user's details as a <see cref="UserDto"/>.</returns>
    Task<UserDto> GetUserByIdAsync(long userId);

    /// <summary>
    /// get user parent async
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<UserDto?> GetUserParentAsync(long userId);
}