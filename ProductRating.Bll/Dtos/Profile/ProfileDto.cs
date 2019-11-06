namespace ProductRating.Bll.Dtos.Profile
{
    public class ProfileDto
    {
        public string NickName { get; set; }

        public string Email { get; set; }

        public PictureDto Avatar { get; set; }

        public string Nationality { get; set; }

        public string Introduction { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }

        public bool IsMine { get; set; }
    }
}
