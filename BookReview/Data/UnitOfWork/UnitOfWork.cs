using BookReview.Data.Models;
using BookReview.Data.Repositories;
using BookReview.Data.Repositories.Interfaces;

namespace BookReview.Data.UnitOfWork
{
    public class UnitOfWork<T> 
        : IUnitOfWork<T>
        where T:BaseEntity
    {

        private readonly BookDbContext _dbContext;
        private readonly Lazy<IGenericRepository<T>> _genericRepository;
        public UnitOfWork(BookDbContext context, IGenericRepository<T> genericRepository)
        { 
            this._dbContext = context;
            this._genericRepository = new Lazy<IGenericRepository<T>>(()=> new GenericRepository<T>(_dbContext));
        }

        public IGenericRepository<T> GenericRepository => _genericRepository.Value;
        public async Task<int> saveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
