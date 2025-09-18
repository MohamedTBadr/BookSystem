using BookReview.Data.DTO.BooksDTOs;
using BookReview.Data.Models;
using BookReview.Data.Repositories;
using System.Linq.Expressions;

namespace BookReview.Services.Specifications
{
    public class BookWithAuthorSpecification:Specifications<Book>
    {
        //Get Book By Id
        public BookWithAuthorSpecification(Guid id):base(b=>b.Id==id)
        {
            //Add Includes
            AddInclude(x => x.Category);
            AddInclude(b => b.Author);
        }
        //Get All Books Use for sorting ,filtration
        public BookWithAuthorSpecification(BookParametersSpecification parametersSpecification) :base( x
            =>
        (!parametersSpecification.AuthorId.HasValue || x.AuthorId==parametersSpecification.AuthorId.Value )
        &&
        (!parametersSpecification.CategoryId.HasValue || x.CategoryId== parametersSpecification.CategoryId.Value)

        )
        {
            AddInclude(x => x.Category);
            AddInclude(b => b.Author);
            ApplyPagination(parametersSpecification.PageIndex, parametersSpecification.PageIndex);
        }
    }
}
