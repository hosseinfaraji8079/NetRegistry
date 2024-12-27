using System.ComponentModel.DataAnnotations;
using Registry.API.Common;

namespace Registry.API.Models;

/// <summary>
/// Represents a user in the system.
/// </summary>
public class User : EntityBase
{
    /// <summary>
    /// Gets or sets the unique chat ID associated with the user.
    /// </summary>
    [Required(ErrorMessage = "Chat ID is required.")]
    public long ChatId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [Display(Name = "First Name")]
    [MaxLength(100, ErrorMessage = "{0} cannot exceed {1} characters.")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [Display(Name = "Last Name")]
    [MaxLength(100, ErrorMessage = "{0} cannot exceed {1} characters.")]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Display(Name = "Username")]
    [MaxLength(50, ErrorMessage = "{0} cannot exceed {1} characters.")]
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "{0} must be at least {1} characters.")]
    [MaxLength(100, ErrorMessage = "{0} cannot exceed {1} characters.")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the roles associated with the user.
    /// </summary>
    public ICollection<UserRole>? UserRoles { get; set; }

    /// <summary>
    /// Gets or sets the collection of registries associated with the user.
    /// </summary>
    public ICollection<Registry>? Registries { get; set; }
}
