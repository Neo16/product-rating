namespace ProductRating.Bll.Dtos
{
    public class PaginationDto
    {
        /// <summary>
        /// Starting from Zero
        /// </summary>
        public int? Start { get; set; }

        public int? Length { get; set; }
    }
}
