using BookReview.Data.DTO.AuthorDTOs;
using BookReview.Services.Common;
using System.Collections.Generic;

namespace BookReview.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<ServiceResult<IEnumerable<AuthorDTO>>> GetAllAsync();
        Task<ServiceResult<AuthorDTO>> GetByIdAsync(Guid Id);

        Task<ServiceResult<AuthorCreateDTO>> CreateAuthorAsync(AuthorCreateDTO CreateDTO);

        Task UpdateAuthorAsync(AuthorUpdateDTO UpdateDTO);


        Task DeleteAuthorAsync(Guid id);


    }
}
