using Registry.API.Enums;

namespace Registry.API.ViewModel;

public class RegistryDto
{
    public long Id { get; set; }
    public string? ImeI_1 { get; set; }
    public string? ImeI_2 { get; set; }
    public string? Summery { get; set; }
    public string? ForWho { get; set; }
    public RegistryStatus? Status { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public long? CreateBy { get; set; }
    public string? Phone { get; set; }
    public string? Model { get; set; }
    public long? Price { get; set; }
    public long? UserId { get; set; }
    public string? PaymentLink { get; set; }
}