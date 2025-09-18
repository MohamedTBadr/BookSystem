namespace BookReview.Data.DTO.BooksDTOs
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string AuthorName { get; set; }
        public string CategoryName { get; set; }

    }
}
