using BookReview.Data.DTO.AuthorDTOs;
using BookReview.Data.Models;
using BookReview.Data.Repositories;
using System.Linq.Expressions;

namespace BookReview.Services.Specifications
{
    public class AuthorSpecifications : Specifications<Author>
    {
        public AuthorSpecifications(Expression<Func<Author, bool>>? criteria) : base(criteria)
        {
           
        }


        //public AuthorSpecifications(AuthorSpecificationParameters parameters) : base()
        //{

        //}
    }
}
