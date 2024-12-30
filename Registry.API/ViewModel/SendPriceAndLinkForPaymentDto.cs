using System.ComponentModel.DataAnnotations;

namespace Registry.API.ViewModel;

/// <summary>
/// Data model for sending price and payment link information.
/// </summary>
public class SendPriceAndLinkForPaymentDto
{
    /// <summary>
    /// The unique identifier.
    /// </summary>
    [Required(ErrorMessage = "Id is required.")]
    [Range(1, long.MaxValue, ErrorMessage = "Id must be greater than zero.")]
    public long Id { get; set; }

    /// <summary>
    /// The amount to be paid.
    /// </summary>
    [Required(ErrorMessage = "Price is required.")]
    [Range(0, long.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
    public long Price { get; set; }

    /// <summary>
    /// The associated payment link.
    /// </summary>
    [Required(ErrorMessage = "PaymentLink is required.")]
    [Url(ErrorMessage = "PaymentLink must be a valid URL.")]
    public string PaymentLink { get; set; }
}
