using System.ComponentModel.DataAnnotations;
using Registry.API.Models;

namespace Registry.API.ViewModel;

public class UserDto
{
    public long? Balance { get; set; }
    
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
    /// id for user
    /// </summary>
    public long Id { get; set; }
}