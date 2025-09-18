namespace BookReview.Data.Models
{
    public class BaseEntity
    {

        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
