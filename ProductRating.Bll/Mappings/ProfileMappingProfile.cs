using AutoMapper;
using ProductRating.Bll.Dtos.Profile;
using ProductRating.Dal.Model.Identity;

namespace ProductRating.Bll.Mappings
{
    public class ProfileMappingProfile : Profile
    {
        public ProfileMappingProfile()
        {
            CreateMap<ApplicationUser, ProfileDto>()
                .ForMember(e => e.NickName, e => e.MapFrom(f => f.NickName))
                .ForMember(e => e.Email, e => e.MapFrom(f => f.Email))
                .ForMember(e => e.Avatar, e => e.MapFrom(f => f.Avatar))
                .ForMember(e => e.Nationality, e => e.MapFrom(f => f.Nationality))
                .ForMember(e => e.Introduction, e => e.MapFrom(f => f.Introduction));
        }
    }
}
