using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BookReview.Data.Repositories
{
    public abstract class Specifications<T>(Expression<Func<T, bool>>? criteria) where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; private set; } = criteria;
        public List<Expression<Func<T, object>>> Includes { get; } = [];
         public int Take { get; private set; } 
        public int Skip { get; private set; } 
        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T,object>> expression) 
            =>Includes.Add(expression);
        protected void ApplyPagination(int pageIndex, int pageSize) 
        {
            Skip = (pageIndex-1)*pageSize;
            Take = pageSize;
            IsPaginated = true;
        }
    }   


}
