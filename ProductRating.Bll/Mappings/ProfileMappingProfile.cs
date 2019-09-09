using AutoMapper;
using ProductRating.Bll.Dtos.Profile;
using ProductRating.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Mappings
{
    public class ProfileMappingProfile : Profile
    {
        public ProfileMappingProfile()
        {
            CreateMap<ApplicationUser, ProfileDto>()
                .ForMember(e => e.NickName, e => e.MapFrom(f => f.NickName))
                .ForMember(e => e.Email, e => e.MapFrom(f => f.Email))
                .ForMember(e => e.Avatar, e => e.MapFrom(f => Convert.ToBase64String(f.Avatar.Data)))
                .ForMember(e => e.Natinality, e => e.MapFrom(f => f.Natinality))
                .ForMember(e => e.Introduction, e => e.MapFrom(f => f.Introduction));
        }
    }
}
