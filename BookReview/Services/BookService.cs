using AutoMapper;
using BookReview.Data.DTO;
using BookReview.Data.DTO.BooksDTOs;
using BookReview.Data.ErrorModels;
using BookReview.Data.Models;
using BookReview.Data.Repositories;
using BookReview.Data.Repositories.Interfaces;
using BookReview.Data.UnitOfWork;
using BookReview.Services.Common;
using BookReview.Services.Interfaces;
using BookReview.Services.Specifications;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web.Http.ModelBinding;

namespace BookReview.Services
{
    public class BookService(IGenericRepository<Book> bookRepository,IMapper mapper,IUnitOfWork<Book> unitOfWork) : IBookService
    {
        public async Task<ServiceResult<BookCreateDTO>> Add(BookCreateDTO bookCreateDTO)
        {
            try
            {


            var book=  mapper.Map<BookCreateDTO,Book>(bookCreateDTO);
                 bookRepository.Add(book);
          var affectedRows=  await unitOfWork.saveChangesAsync();
                if (affectedRows==0)
                {
                    throw new Exception();
                }
                return ServiceResult<BookCreateDTO>.Success(bookCreateDTO,"Added Successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //return ServiceResult<BookCreateDTO>.Failure($"{ex.Message}");
            }
        }

        public async Task  Delete(Guid id)
        {
            var book=await  bookRepository.GetById(id);
            if (book == null)
            {
                throw new BookNotFoundException(id);
            }
            bookRepository.Delete(book);
            await unitOfWork.saveChangesAsync();
        }

        public async Task<ServiceResult<PagedResult<BookDTO>>> GetAll(BookParametersSpecification parametersSpecification)
        {
          
                var specifications = new BookWithAuthorSpecification(parametersSpecification);
          var books=await bookRepository.GetAllAsync(specifications);
         var booksDto= mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(books);
            var totalCount =await bookRepository.CountAsync(new BookCountSpecification(parametersSpecification));
            var data = new PagedResult<BookDTO>(booksDto,totalCount,parametersSpecification.PageIndex,parametersSpecification.PageSize);
           
                return ServiceResult<PagedResult<BookDTO>>.Success(data,"Books Retrieved Successfully");
                  
           



        }

      

        public async Task<ServiceResult<BookDTO>> GetById(Guid id)
        {
           
                var specifications= new BookWithAuthorSpecification(id);
                var book = await bookRepository.GetById(specifications);
                if (book == null)
                {
                throw new BookNotFoundException(id);
                }
                var MappedBook = mapper.Map<Book, BookDTO>(book);
                return ServiceResult<BookDTO>.Success(MappedBook, "Book Retrieved Successfully");
        
        }

        public async Task Update(BookUpdateDTO bookUpdate)
        {
         
            var book = await bookRepository.GetById(bookUpdate.Id);
            if (book == null)
            {
                throw new BookNotFoundException(bookUpdate.Id);
            }
           
                bookRepository.Update(book);
                await unitOfWork.saveChangesAsync();
            
        }
    }
}
