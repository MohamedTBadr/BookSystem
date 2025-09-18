using System.ComponentModel.DataAnnotations;

namespace BookReview.Data.DTO.BooksDTOs
{
    public class BookCreateDTO
    {

        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
        [Required(ErrorMessage = "Price is required")]

        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price {  get; set; }
       

    }
}
