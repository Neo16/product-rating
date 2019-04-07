using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos.Profile;
using ProductRating.Bll.Exceptions;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
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

        public ProfileService(
            ApplicationDbContext context,
            MapperConfiguration mapperConfiguration)
            :base(context)
        {
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<ProfileDto> GetProfileByUserId(Guid UserId)
        {
            return await context.Users
                .Where(e => e.Id == UserId)
                .ProjectTo<ProfileDto>(mapperConfiguration)
                .SingleAsync();               
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
            user.AvatarId = profile.PictureId;
            user.Natinality = profile.Natinality;
            user.Introduction = profile.Introduction;

            await context.SaveChangesAsync();

        }
    }
}
