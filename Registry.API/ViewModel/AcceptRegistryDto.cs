using System.ComponentModel.DataAnnotations;

namespace Registry.API.ViewModel;

/// <summary>
/// Data Transfer Object for making a decision (accept or reject) on a registry entry.
/// </summary>
public class RegistryDecisionDto : IValidatableObject
{
    /// <summary>
    /// Gets or sets the unique identifier of the registry entry.
    /// </summary>
    [Required(ErrorMessage = "Registry ID is required.")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the model information. If provided, the registry will be accepted.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Gets or sets the predefined rejection reason ID. Required if Model is not provided.
    /// </summary>
    public long? PredefinedRejectionReasonId { get; set; }

    /// <summary>
    /// Gets or sets additional explanations provided during rejection.
    /// </summary>
    [MaxLength(500, ErrorMessage = "Additional explanation cannot exceed 500 characters.")]
    public string? AdditionalExplanation { get; set; }

    /// <summary>
    /// Validates the DTO to ensure either Model is provided or PredefinedRejectionReasonId is set.
    /// </summary>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>A collection of validation results.</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Model))
        {
            if (!PredefinedRejectionReasonId.HasValue)
            {
                yield return new ValidationResult(
                    "Predefined rejection reason ID is required when Model is not provided.",
                    new[] { nameof(PredefinedRejectionReasonId) }
                );
            }
        }
    }
}