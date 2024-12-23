using AutoMapper;
using Registry.API.ViewModel;

namespace Registry.API.Mappings;

public class RegistryProfiler : Profile
{
    public RegistryProfiler()
    {
        CreateMap<AddRegistryDto, Models.Registry>();
        CreateMap<RegistryDto, Models.Registry>().ReverseMap();
    }
}