namespace BookReview.Data.Models
{
    public class Author:BaseEntity
    {
        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }
        
        public int AuthorAge {  get; set; }

        public string Image {  get; set; }
    }
}
