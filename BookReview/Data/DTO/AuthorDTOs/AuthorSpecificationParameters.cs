namespace BookReview.Data.DTO.AuthorDTOs
{
    public class AuthorSpecificationParameters
    {
        public int PageIndex { get; set; } = 1;

        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int _pageSize = DefaultPageSize;

        public string SerachItem { get; set; }

    


        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;

        }
    }
}
