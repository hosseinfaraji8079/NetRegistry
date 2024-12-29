using System.ComponentModel.DataAnnotations;
using Registry.API.Common;

namespace Registry.API.Models;

/// <summary>
/// Represents a predefined rejection reason.
/// </summary>
public class PredefinedRejectionReason : EntityBase
{
    /// <summary>
    /// Gets or sets the description of the predefined rejection reason.
    /// </summary>
    [Required(ErrorMessage = "Reason is required.")]
    [MaxLength(150, ErrorMessage = "Reason cannot exceed {0} characters.")]
    public string? Reason { get; set; }
}