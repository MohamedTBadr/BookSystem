using BookReview.Data.DTO;
using BookReview.Data.DTO.BooksDTOs;
using BookReview.Data.Models;
using BookReview.Services.Common;

namespace BookReview.Data.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        public Task Add(T TEntity);
        public void Update(T TEntity);
        public void Delete(T TEntity);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllAsync(Specifications<T> specifications);
        public Task<int> CountAsync(Specifications<T> specifications);
        public Task<T> GetById(Guid id);
        public Task<T> GetById(Specifications<T> specifications);

    }
}
