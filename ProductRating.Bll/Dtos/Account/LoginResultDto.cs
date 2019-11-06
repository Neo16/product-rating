using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Account
{
    public class LoginResultDto
    {
        public string UserToken { get; set; }

        public string UserName { get; set; }

        public List<string> UserRoles { get; set; }

        public Guid UserId { get; set; }
    }
}
