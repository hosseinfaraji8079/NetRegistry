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

        CreateMap<Models.Registry, UpdateRegistryDto>().ReverseMap();
        
        CreateMap<Models.Registry, RegistryDto>()
            .ForMember(dest => dest.Reason,
                opt => opt.MapFrom(src => src.RejectionReasons.LastOrDefault().PredefinedRejectionReason.Reason))
            .ForMember(dest => dest.AdditionalExplanation,
                opt => opt.MapFrom(src => src.RejectionReasons.LastOrDefault().AdditionalExplanation));

        CreateMap<User, UserDto>().ReverseMap();
        
        CreateMap<PredefinedRejectionReason, PredefinedRejectionReasonDto>().ReverseMap();
    }
}