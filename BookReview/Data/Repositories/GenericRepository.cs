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


          public async Task<T> GetById(Guid id) => await dbContext.Set<T>().FirstOrDefaultAsync(b => b.Id == id);

        public async Task<T> GetById(Specifications<T> specifications)
            =>
        await SpecificationEvaluator.CreateQuery(dbContext.Set<T>(), specifications).FirstOrDefaultAsync();



        public void Update(T TEntity) => dbContext.Set<T>().Update(TEntity);
    }
}
