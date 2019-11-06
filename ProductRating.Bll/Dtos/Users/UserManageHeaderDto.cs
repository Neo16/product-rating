using System;

namespace ProductRating.Bll.Dtos.Users
{
    public class UserManageHeaderDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string NickName { get; set; }

        public string Role { get; set; }

        public bool IsLockedOut { get; set; }
    }
}
