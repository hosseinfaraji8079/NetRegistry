using Registry.API.Enums;
using Registry.API.ViewModel.Paging;

namespace Registry.API.ViewModel;

public class FilterRegistryDto : BasePaging<RegistryDto>
{
    public string? Imei { get; set; }
    public RegistryStatus? Status { get; set; }
}
