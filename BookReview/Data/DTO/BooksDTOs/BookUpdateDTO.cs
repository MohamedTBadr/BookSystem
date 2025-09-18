using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BookReview.Data.DTO.BooksDTOs
{
    public class BookUpdateDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Title is Required")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Description is required")]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage ="Price Is Required")]

        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }


    }
}
