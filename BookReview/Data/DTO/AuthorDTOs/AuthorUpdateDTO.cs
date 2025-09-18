namespace BookReview.Data.DTO.AuthorDTOs
{
    public class AuthorUpdateDTO
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }

        public int AuthorAge { get; set; }

        public string Image { get; set; }

    }
}
