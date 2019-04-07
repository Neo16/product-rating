using ProductRating.Bll.Dtos.Profile;
using System;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IProfileService
    { 
         Task<ProfileDto> GetProfileByUserId(Guid UserId);

         Task UpdateProfile(Guid UserId, EditProfileDto profile);
    }
}
