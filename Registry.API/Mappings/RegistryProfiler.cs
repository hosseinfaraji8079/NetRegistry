using AutoMapper;
using Registry.API.Models;
using Registry.API.ViewModel;

namespace Registry.API.Mappings;

public class RegistryProfiler : Profile
{
    public RegistryProfiler()
    {
        CreateMap<AddRegistryDto, Models.Registry>();
        CreateMap<User, AddUserDto>().ReverseMap();
        CreateMap<RegistryDto, Models.Registry>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}