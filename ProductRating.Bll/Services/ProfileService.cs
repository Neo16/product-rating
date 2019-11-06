using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos.Profile;
using ProductRating.Bll.Exceptions;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Common;
using ProductRating.Dal;
using ProductRating.Dal.Model.Identity;
using System;
using System.Linq;
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
            : base(context)
        {
            this.mapperConfiguration = mapperConfiguration;
            this.mapper = mapper;
        }

        public async Task<ProfileDto> GetProfileByUserId(Guid UserId)
        {
            var ownerRole = await context.Set<ApplicationRole>().SingleAsync(e => e.Name == RoleNames.SHOP_OWNER_ROLE);
            var adminRole = await context.Set<ApplicationRole>().SingleAsync(e => e.Name == RoleNames.ADMIN_ROLE);

            var user = await context.Users
                .Include(e => e.Avatar)
                .Where(e => e.Id == UserId)
                .SingleOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var profile = mapper.Map<ProfileDto>(user);
            if (context.UserRoles.Where(d => d.UserId == user.Id).Any(e => e.RoleId == adminRole.Id))
            {
                profile.Role = "Administrator";
            }
            else if (context.UserRoles.Where(d => d.UserId == user.Id).Any(e => e.RoleId == ownerRole.Id))
            {
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
            if (profile.PictureId != null)
            {
                user.AvatarId = profile.PictureId;
            }
            user.Nationality = profile.Nationality;
            user.Introduction = profile.Introduction;

            await context.SaveChangesAsync();

        }
    }
}
