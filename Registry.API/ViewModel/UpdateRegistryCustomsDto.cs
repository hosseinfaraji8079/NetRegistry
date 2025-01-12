using System.ComponentModel.DataAnnotations;

namespace Registry.API.ViewModel;

public class UpdateRegistryCustomsDto
{
    public long Id { get; set; }
    
    /// <summary>
    /// National ID of the traveler.
    /// </summary>
    [MaxLength(length: 11, ErrorMessage = "this filed max length {0}")]
    [MinLength(length: 10, ErrorMessage = "this filed min length {0}")]
    public string? TravelerNationalId { get; set; }

    /// <summary>
    /// Phone number of the traveler.
    /// </summary>
    public string? TravelerPhone { get; set; }

    /// <summary>
    /// Birthdate of the traveler.
    /// </summary>
    public string? TravelerBirthDate { get; set; }

    /// <summary>
    /// Customs IBAN number.
    /// </summary>
    public string? CustomsIBAN { get; set; }

    /// <summary>
    /// Customs payment identifier.
    /// </summary>
    public string? CustomsPaymentId { get; set; }

    /// <summary>
    /// Customs amount.
    /// </summary>
    public long? CustomsAmount { get; set; }
}