using System.ComponentModel.DataAnnotations;
using Registry.API.Common;

namespace Registry.API.Models;

/// <summary>
/// Represents a permission entity with optional hierarchical structure and role associations.
/// </summary>
public class Permission : EntityBase
{
    /// <summary>
    /// Gets or sets the identifier of the parent permission, if applicable.
    /// </summary>
    public long? ParentId { get; set; } = null;

    /// <summary>
    /// Gets or sets the display name of the permission.
    /// </summary>
    [Display(Name = "Display Name")]
    [Required(ErrorMessage = "Please provide the {0}.")]
    [MaxLength(300, ErrorMessage = "{0} cannot exceed {1} characters.")]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the system name of the permission, used for internal purposes.
    /// </summary>
    [Display(Name = "System Name")]
    [Required(ErrorMessage = "Please provide the {0}.")]
    [MaxLength(300, ErrorMessage = "{0} cannot exceed {1} characters.")]
    public string? SystemName { get; set; }
    
    /// <summary>
    /// Gets or sets the parent permission object, if applicable.
    /// </summary>
    public Permission? Parent { get; set; } = null;

    /// <summary>
    /// Gets or sets the collection of role-permission associations.
    /// </summary>
    public ICollection<RolePermission>? RolePermissions { get; set; }
}