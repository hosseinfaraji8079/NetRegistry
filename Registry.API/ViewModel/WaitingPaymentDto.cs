namespace Registry.API.ViewModel;

public class WaitingPaymentDto
{
    public long UserId { get; set; }
    public long RegistryId { get; set; }
    public DateTime Time { get; set; } = DateTime.UtcNow;
}