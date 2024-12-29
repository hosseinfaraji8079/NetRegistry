namespace Registry.API.ViewModel;

public class SendPriceAndLinkForPaymentDto
{
    public long Price { get; set; }
    public string? PaymentLink { get; set; }
}