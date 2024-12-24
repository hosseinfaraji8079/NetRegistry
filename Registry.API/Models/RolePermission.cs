using Registry.API.Common;

namespace Registry.API.Models;

/// <summary>
/// Represents the association between a role and a permission.
/// This entity defines which permissions are assigned to a specific role.
/// </summary>
public class RolePermission : EntityBase
{
    /// <summary>
    /// Gets or sets the unique identifier of the role.
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the permission.
    /// </summary>
    public long PermissionId { get; set; }

    /// <summary>
    /// Gets or sets the role associated with this RolePermission.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Gets or sets the permission associated with this RolePermission.
    /// </summary>
    public Permission Permission { get; set; }
}