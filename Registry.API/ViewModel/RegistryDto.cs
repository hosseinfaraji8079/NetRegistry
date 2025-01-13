using Registry.API.Enums;

namespace Registry.API.ViewModel;

public class RegistryDto
{
    /// <summary>
    /// A unique identifier for each registry entry.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The first IMEI (International Mobile Equipment Identity) number of the device.
    /// </summary>
    public string? ImeI_1 { get; set; }

    /// <summary>
    /// The second IMEI number of the device (if dual SIM).
    /// </summary>
    public string? ImeI_2 { get; set; }

    /// <summary>
    /// A summary or brief description of the registry entry.
    /// </summary>
    public string? Summery { get; set; }

    /// <summary>
    /// Indicates who this registry entry is intended for.
    /// </summary>
    public string? ForWho { get; set; }

    /// <summary>
    /// The status of the registry (e.g., Active, Inactive, Pending Approval).
    /// </summary>
    public RegistryStatus? Status { get; set; }

    /// <summary>
    /// The date when the registry entry was created.
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// The last modification date of the registry entry.
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// The user ID of the person who created the registry entry.
    /// </summary>
    public long? CreateBy { get; set; }

    /// <summary>
    /// The phone number associated with the registry entry.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// The model of the device or product.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// The price associated with the registry entry.
    /// </summary>
    public long? Price { get; set; }

    /// <summary>
    /// The user ID associated with this registry entry.
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// The payment link for completing transactions related to this registry.
    /// </summary>
    public string? PaymentLink { get; set; }

    /// <summary>
    /// The reason associated with this registry entry (e.g., for rejection or additional notes).
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Additional explanation or remarks related to the registry entry.
    /// </summary>
    public string? AdditionalExplanation { get; set; }
    
    
    /// <summary>
    /// National ID of the traveler.
    /// </summary>
    public string? TravelerNationalId { get; set; }

    /// <summary>
    /// Phone number of the traveler.
    /// </summary>
    public string? TravelerPhone { get; set; }

    /// <summary>
    /// Birthdate of the traveler.
    /// </summary>
    public DateTime? TravelerBirthDate { get; set; }

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

    /// <summary>
    /// Profit Order
    /// </summary>
    public long Profit { get; set; }
}
