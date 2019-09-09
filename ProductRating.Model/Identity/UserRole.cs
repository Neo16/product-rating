using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Identity
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationRole Role { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
