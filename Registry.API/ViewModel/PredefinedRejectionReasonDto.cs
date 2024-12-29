using System.ComponentModel.DataAnnotations;

namespace Registry.API.ViewModel;

/// <summary>
/// Data Transfer Object for predefined rejection reasons.
/// </summary>
public class PredefinedRejectionReasonDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the predefined rejection reason.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the description of the predefined rejection reason.
    /// </summary>
    [Required(ErrorMessage = "Reason is required.")]
    [MaxLength(150, ErrorMessage = "Reason cannot exceed 150 characters.")]
    public string Reason { get; set; }
}