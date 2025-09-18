namespace BookReview.Data.DTO.BooksDTOs
{
    public class BookParametersSpecification
    {
        public Guid? AuthorId { get; set; }
        public Guid? CategoryId { get; set; }

        public int PageIndex { get; set; } = 1;
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int _pageSize = DefaultPageSize;


        public int PageSize {
            get => _pageSize;
            set => _pageSize=value>MaxPageSize ?MaxPageSize : value;
        
        }
    }
}
