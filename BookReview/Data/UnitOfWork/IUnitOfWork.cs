using BookReview.Data.Repositories.Interfaces;

namespace BookReview.Data.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        Task<int> saveChangesAsync();

        IGenericRepository<T> GenericRepository { get; }
    }
}
