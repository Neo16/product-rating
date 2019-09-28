using ProductRating.Bll.Dtos.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Users
{
    public class UserManageFilterDto
    {
        public string Email { get; set; }

        public string NickName { get; set; }

        public Role? Role { get; set; }

        public bool? IsLockedOut { get; set; }
    }
}
