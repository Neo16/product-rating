using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos.Profile;
using ProductRating.Bll.Exceptions;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Common;
using ProductRating.Dal;
using ProductRating.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class ProfileService : ServiceBase, IProfileService
    {
      
        private readonly MapperConfiguration mapperConfiguration;
        private readonly IMapper mapper; 

        public ProfileService(
            IMapper mapper,
            ApplicationDbContext context,
            MapperConfiguration mapperConfiguration)
            :base(context)
        {          
            this.mapperConfiguration = mapperConfiguration;
            this.mapper = mapper;
        }

        public async Task<ProfileDto> GetProfileByUserId(Guid UserId)
        {
            var user = await context.Users
                .Include(e => e.Roles)
                .ThenInclude(e => e.Role)
                .Where(e => e.Id == UserId)
                .SingleOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var profile = mapper.Map<ProfileDto>(user); 
            if (user.Roles.Any(e => e.Role.Name == RoleNames.ADMIN_ROLE))
            {
                profile.Role = "Administrator";
            }
            else if(user.Roles.Any(e => e.Role.Name == RoleNames.SHOP_OWNER_ROLE)) {
                profile.Role = "Webshop owner";
            }
            else
            {
                profile.Role = "Customer";
            }

            return profile;                    
        }

        public async Task UpdateProfile(Guid UserId, EditProfileDto profile)
        {
            var user = await context.Users             
                .SingleAsync(e => e.Id == UserId);

            if (user == null)
            {
                throw new BusinessLogicException("User does not exists.") { ErrorCode = ErrorCode.InvalidArgument };
            }

            user.NickName = profile.NickName;
            user.Email = profile.Email;
            if (profile.PictureId != null){
                user.AvatarId = profile.PictureId;
            }      
            user.Nationality = profile.Nationality;
            user.Introduction = profile.Introduction;

            await context.SaveChangesAsync();

        }
    }
}
