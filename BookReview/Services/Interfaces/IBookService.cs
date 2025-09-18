using BookReview.Data.DTO;
using BookReview.Data.DTO.BooksDTOs;
using BookReview.Services.Common;
using System.Linq;

namespace BookReview.Services.Interfaces
{
    public interface IBookService
    {
        //Get All,GetById,Add,Remove,Update
        Task<ServiceResult<BookCreateDTO>> Add(BookCreateDTO bookCreateDTO);
        Task Delete(Guid id);
        Task Update(BookUpdateDTO bookCreateDTO);

        Task<ServiceResult<PagedResult<BookDTO>>> GetAll(BookParametersSpecification parametersSpecification);
        Task<ServiceResult<BookDTO>> GetById(Guid id);
    }
}
