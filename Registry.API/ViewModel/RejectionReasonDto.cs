using System.ComponentModel.DataAnnotations;

namespace Registry.API.ViewModel;

/// <summary>
/// Data Transfer Object for submitting a rejection reason.
/// </summary>
public class RejectionReasonDto
{
    /// <summary>
    /// Gets or sets the ID of the predefined rejection reason selected by the user.
    /// </summary>
    [Required(ErrorMessage = "Predefined rejection reason ID is required.")]
    public long PredefinedRejectionReasonId { get; set; }

    /// <summary>
    /// Gets or sets additional explanations provided by the support team.
    /// </summary>
    [MaxLength(500, ErrorMessage = "Additional explanation cannot exceed 500 characters.")]
    public string? AdditionalExplanation { get; set; }
}