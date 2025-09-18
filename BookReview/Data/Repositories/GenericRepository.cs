using AutoMapper;
using BookReview.Data.DTO;
using BookReview.Data.DTO.BooksDTOs;
using BookReview.Data.Models;
using BookReview.Data.Repositories.Interfaces;
using BookReview.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace BookReview.Data.Repositories
{
    public class GenericRepository<T>(BookDbContext dbContext)
        : IGenericRepository<T> 
        where T:BaseEntity
    {

        public async Task Add(T TEntity)
        {
            await dbContext.Set<T>().AddAsync(TEntity);
        }

        public async Task<int> CountAsync(Specifications<T> specifications) => await SpecificationEvaluator.CreateQuery(dbContext.Set<T>(), specifications).CountAsync();



        public void Delete(T TEntity)
        {
          TEntity.IsDeleted = true;

            dbContext.Set<T>().Update(TEntity);
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await dbContext.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(Specifications<T> specifications)
            =>
        await SpecificationEvaluator.CreateQuery(dbContext.Set<T>(), specifications).ToListAsync();


        #region WIP
        //public async Task<ServiceResult<PagedList<BookDTO>>> GetAllAsync(PaginationParams queryParams)
        //{
        //    IQueryable<Book> query = dbContext.Books;

        //    // Searching
        //    if (!string.IsNullOrEmpty(queryParams.Search))
        //    {
        //        var lowerTerm = queryParams.Search.ToLower();
        //        //query = query.Where(b => b.Title.ToLower().Contains(lowerTerm)
        //        //                      || b.Author.ToLower().Contains(lowerTerm));
        //    }

        //    // Sorting
        //    if (!string.IsNullOrEmpty(queryParams.SortBy))
        //    {
        //        // Use reflection or switch for known properties
        //        query = queryParams.SortBy.ToLower() switch
        //        {
        //            "title" => queryParams.SortDescending ? query.OrderByDescending(b => b.Title) : query.OrderBy(b => b.Title),
        //            //"author" => queryParams.SortDescending ? query.OrderByDescending(b => b.Author) : query.OrderBy(b => b.Author),
        //            "publisheddate" => queryParams.SortDescending ? query.OrderByDescending(b => b.CreatedAt) : query.OrderBy(b => b.CreatedAt),
        //            _ => query
        //        };
        //    }

        //    // Pagination
        //    var pagedBooks = await PagedList<Book>.CreateAsync(query, queryParams.PageNumber, queryParams.PageSize);

        //    // Map to DTOs (assuming you have a mapper)
        //    var bookDtos = mapper.Map<IEnumerable<BookDTO>>(pagedBooks);

        //    return ServiceResult<PagedList<BookDTO>>.Success(pagedBooks);
        //}
        #endregion
        public async Task<T> GetById(Guid id) => await dbContext.Set<T>().FirstOrDefaultAsync(b => b.Id == id);

        public async Task<T> GetById(Specifications<T> specifications)
            =>
        await SpecificationEvaluator.CreateQuery(dbContext.Set<T>(), specifications).FirstOrDefaultAsync();



        public void Update(T TEntity) => dbContext.Set<T>().Update(TEntity);
    }
}
