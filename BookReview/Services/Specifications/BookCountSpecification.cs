using BookReview.Data.DTO.BooksDTOs;
using BookReview.Data.Models;
using BookReview.Data.Repositories;

namespace BookReview.Services.Specifications
{
    public class BookCountSpecification:Specifications<Book>
    {
     
        //Get All Books Use for sorting ,filtration
        public BookCountSpecification(BookParametersSpecification parametersSpecification) : base(x
            =>
        (!parametersSpecification.AuthorId.HasValue || x.AuthorId == parametersSpecification.AuthorId.Value)
        &&
        (!parametersSpecification.CategoryId.HasValue || x.CategoryId == parametersSpecification.CategoryId.Value)

        )
        {
           
        }
    }
}
