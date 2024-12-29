using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Registry.API.Common;
using Registry.API.Enums;

namespace Registry.API.Models;

/// <summary>
/// Represents a registry entity in the system, holding information about a user or transaction.
/// </summary>
public class Registry : EntityBase
{
    /// <summary>
    /// Gets or sets the first IMEI number (International Mobile Equipment Identity).
    /// </summary>
    [Required(ErrorMessage = "IMEI 1 is required.")]
    [MaxLength(16, ErrorMessage = "IMEI 1 cannot exceed 16 characters.")]
    public string? ImeI_1 { get; set; }

    /// <summary>
    /// Gets or sets the second IMEI number.
    /// </summary>
    [MaxLength(16, ErrorMessage = "IMEI 2 cannot exceed 16 characters.")]
    public string? ImeI_2 { get; set; }

    /// <summary>
    /// Gets or sets the flag indicating whether the user accepts the rules.
    /// Default is true.
    /// </summary>
    [Required(ErrorMessage = "You must accept the rules.")]
    public bool AcceptTheRules { get; set; } = true;

    /// <summary>
    /// Gets or sets the summary or description for the registry.
    /// </summary>
    [MaxLength(500, ErrorMessage = "Summary cannot exceed 500 characters.")]
    public string? Summery { get; set; } = "";

    /// <summary>
    /// Gets or sets the target audience for the registry.
    /// </summary>
    [MaxLength(50, ErrorMessage = "Target audience cannot exceed 50 characters.")]
    public string? ForWho { get; set; }

    /// <summary>
    /// Gets or sets the phone number associated with the registry.
    /// This field is required and limited to 11 characters.
    /// </summary>
    [Required(ErrorMessage = "Phone number is required.")]
    [MaxLength(11, ErrorMessage = "Phone number cannot exceed 11 characters.")]
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the model information associated with the registry.
    /// </summary>
    [MaxLength(50, ErrorMessage = "Model cannot exceed 50 characters.")]
    public string? Model { get; set; }

    /// <summary>
    /// Gets or sets the price associated with the registry.
    /// This is an optional field.
    /// </summary>
    public long? Price { get; set; }

    /// <summary>
    /// payment link
    /// </summary>
    [MaxLength(500)]
    public string? PaymentLink { get; set; }

    /// <summary>
    /// Gets or sets the list of transaction image URLs or paths.
    /// This is an optional field and can be null.
    /// </summary>
    [MaxLength(500, ErrorMessage = "Each transaction image path cannot exceed 500 characters.")]
    public List<string>? TransactionImages { get; set; } = null;

    /// <summary>
    /// Gets or sets the current status of the registry.
    /// Default value is <see cref="RegistryStatus.PendingReview"/>.
    /// </summary>
    public RegistryStatus Status { get; set; } = RegistryStatus.PendingReview;

    /// <summary>
    /// Gets or sets the user associated with this entity.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the user. 
    /// This property serves as a foreign key referencing the <see cref="User"/> entity.
    /// </summary>
    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
    
    /// <summary>
    /// Navigation property to the list of rejection reasons associated with this registry.
    /// </summary>
    public List<RejectionReason>? RejectionReasons { get; set; }
}
