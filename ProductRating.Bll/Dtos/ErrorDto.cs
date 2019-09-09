using ProductRating.Bll.Exceptions;

namespace ProductRating.Bll.Dtos
{
    public class ErrorDto
    {
        public ErrorCode ErrorCode { get; set; }

        public string[] ErrorMessages { get; set; } = new string[0];
    }
}
