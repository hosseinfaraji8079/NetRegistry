using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Registry.API.Common;

namespace Registry.API.Models;

/// <summary>
/// Represents the rejection reason for a specific registry entry.
/// </summary>
public class RejectionReason : EntityBase
{
    /// <summary>
    /// Gets or sets the foreign key to the predefined rejection reason.
    /// </summary>
    [Required(ErrorMessage = "Predefined rejection reason ID is required.")]
    [ForeignKey(nameof(PredefinedRejectionReason))]
    public long PredefinedRejectionReasonId { get; set; }

    /// <summary>
    /// Navigation property to the predefined rejection reason.
    /// </summary>
    public PredefinedRejectionReason PredefinedRejectionReason { get; set; }

    /// <summary>
    /// Gets or sets additional explanations provided by the support team.
    /// </summary>
    [MaxLength(500, ErrorMessage = "Additional explanation cannot exceed 500 characters.")]
    public string? AdditionalExplanation { get; set; }

    /// <summary>
    /// Gets or sets the foreign key to the associated registry entry.
    /// </summary>
    [Required(ErrorMessage = "Registry ID is required.")]
    [ForeignKey(nameof(Registry))]
    public long RegistryId { get; set; }

    /// <summary>
    /// Navigation property to the associated registry entry.
    /// </summary>
    public Registry Registry { get; set; }
}