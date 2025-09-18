using System.ComponentModel.DataAnnotations.Schema;

namespace BookReview.Data.Models
{
    public class Book:BaseEntity
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
        public decimal  Price { get; set; }

        public Author Author { get; set; }
        public Guid AuthorId { get; set; }

        public Category Category { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

    }
}
