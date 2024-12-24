using Registry.API.Common;

namespace Registry.API.Models;

/// <summary>
/// Represents the association between a user and a role.
/// This entity defines which roles are assigned to a specific user.
/// </summary>
public class UserRole : EntityBase
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the role.
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// Gets or sets the user associated with this UserRole.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Gets or sets the role associated with this UserRole.
    /// </summary>
    public Role? Role { get; set; }
}
