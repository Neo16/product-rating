using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Profile
{
    public class ProfileDto
    {
        public string NickName { get; set; }
        
        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public string Natinality { get; set; }       

        public string Introduction  { get; set; }
    }
}
