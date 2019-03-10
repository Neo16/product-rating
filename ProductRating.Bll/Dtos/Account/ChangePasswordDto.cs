using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Account
{
    public class ChangePasswordDto
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }
    }
}
