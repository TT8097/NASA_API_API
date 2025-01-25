using AutoMapper;
using NASASerwerAPI.Models;
using NASASerwerAPI.WebServices;

namespace NASASerwerAPI.MappingProfile
{
    public class MapProfile : Profile
    {
        public MapProfile() {
            CreateMap<APODRaw, APODUser>();

            CreateMap<MissionModel, MissionModelUser>()
                .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.Institution))
                .ForMember(dest => dest.person, opt => opt.MapFrom(src => src.person));

            CreateMap<MissionModel.Person, MissionModelUser.Person>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }
}
