using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Profile
{
    public class EditProfileDto
    {

        public string NickName { get; set; }

        public string Email { get; set; }

        public Guid? PictureId { get; set; }

        public string Nationality { get; set; }

        public string Introduction { get; set; }
    }
}
