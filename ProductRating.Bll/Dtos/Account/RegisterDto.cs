using ProductRating.Bll.Dtos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProductRating.Bll.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string NickName { get; set; }
        [Required]
        public string Password { get; set; }

        public string Natinonality { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
