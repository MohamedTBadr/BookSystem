using AutoMapper;
using BookReview.Data.DTO.AuthorDTOs;
using BookReview.Data.ErrorModels;
using BookReview.Data.Models;
using BookReview.Data.Repositories.Interfaces;
using BookReview.Data.UnitOfWork;
using BookReview.Services.Common;
using BookReview.Services.Interfaces;

namespace BookReview.Services
{
    public class AuthorService(IGenericRepository<Author> authorRepository,IMapper mapper,IUnitOfWork<Author> unitOfWork) : IAuthorService
    {
        public async Task<ServiceResult<AuthorCreateDTO>> CreateAuthorAsync(AuthorCreateDTO CreateDTO)
        {
           
                var author= mapper.Map<Author>(CreateDTO);
                await authorRepository.Add(author);
                var affectedRows = await unitOfWork.saveChangesAsync();
                if (affectedRows == 0)
                {
                    throw new Exception();
                }
                return ServiceResult<AuthorCreateDTO>.Success(CreateDTO,"Created Successfully");
            
          
        }

        public async Task DeleteAuthorAsync(Guid id)
        {
            var Author = await authorRepository.GetById(id);
            if (Author == null) throw new AuthorNotFoundException(id);

            Author.IsDeleted = true;
            Author.UpdatedAt = DateTime.UtcNow;
            await unitOfWork.saveChangesAsync();
        }

        public async Task<ServiceResult<IEnumerable<AuthorDTO>>> GetAllAsync()
        {
         
                var Authors= await authorRepository.GetAllAsync();
                var AuthorsDtos=mapper.Map<IEnumerable<AuthorDTO>>(Authors);
                return ServiceResult<IEnumerable<AuthorDTO>>.Success(AuthorsDtos);
         
        }

        public async Task<ServiceResult<AuthorDTO>> GetByIdAsync(Guid Id)
        {
         
                var Author= await authorRepository.GetById(Id);
            if (Author == null) throw new AuthorNotFoundException(Id);
                var AuthorDto=mapper.Map<AuthorDTO>(Author);
                return ServiceResult<AuthorDTO>.Success(AuthorDto);

           
          
        }

        public async Task UpdateAuthorAsync(AuthorUpdateDTO UpdateDTO)
        {
            var Author = await authorRepository.GetById(UpdateDTO.Id);
            if(Author == null) throw new AuthorNotFoundException(UpdateDTO.Id);
            //var AuthorDto = mapper.Map<AuthorDTO>(Author);
                 authorRepository.Update(Author);
            await unitOfWork.saveChangesAsync();
            

        }
    }
}
