using System.ComponentModel.DataAnnotations;

namespace Registry.API.ViewModel;

/// <summary>
/// Represents a data transfer object (DTO) for user information.
/// </summary>
public class AddUserDto
{
    /// <summary>
    /// parent this user
    /// </summary>
    public AddPrentDto? Parent { get; set; }

    /// <summary>
    /// Gets or sets the unique chat ID associated with the user.
    /// </summary>
    [Required(ErrorMessage = "شناسه چت اجباری است.")]
    public long ChatId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [Display(Name = "نام")]
    [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [Display(Name = "نام خانوادگی")]
    [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Display(Name = "نام کاربری")]
    [MaxLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
    public string? UserName { get; set; }
}

public class AddPrentDto
{
    /// <summary>
    /// Gets or sets the unique chat ID associated with the user.
    /// </summary>
    [Required(ErrorMessage = "شناسه چت اجباری است.")]
    public long ChatId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [Display(Name = "نام")]
    [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [Display(Name = "نام خانوادگی")]
    [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Display(Name = "نام کاربری")]
    [MaxLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
    public string? UserName { get; set; }
}