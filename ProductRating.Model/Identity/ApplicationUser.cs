using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ProductRating.Model.Entities;

namespace ProductRating.Model.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string NickName { get; set; }

        public Picture Avatar { get; set; }

        public Guid? AvatarId { get; set; }

        public string Natinality { get; set; }

        public string Introduction { get; set; }
    }
}
