using System.ComponentModel.DataAnnotations;
using Registry.API.Common;

namespace Registry.API.Models;

/// <summary>
/// Represents a role entity, which defines a set of permissions and associations with users.
/// </summary>
public class Role : EntityBase
{
    /// <summary>
    /// Gets or sets the display name of the role.
    /// </summary>
    [Display(Name = "Display Name")]
    [Required(ErrorMessage = "Please provide the {0}.")]
    [MaxLength(300, ErrorMessage = "{0} cannot exceed {1} characters.")]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the collection of role-permission associations.
    /// This defines the permissions assigned to the role.
    /// </summary>
    public ICollection<RolePermission>? RolePermissions { get; set; }

    /// <summary>
    /// Gets or sets the collection of user-role associations.
    /// This defines the users assigned to the role.
    /// </summary>
    public ICollection<UserRole>? UserRoles { get; set; }
}