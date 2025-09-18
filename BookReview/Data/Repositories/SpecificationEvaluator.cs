using Microsoft.EntityFrameworkCore;

namespace BookReview.Data.Repositories
{
    public static class SpecificationEvaluator
    {
        //Db set ,Specification
        public static IQueryable<T> CreateQuery<T>(IQueryable<T> inputQuery,Specifications<T> specifications)where T : class
        {
            var Query = inputQuery;
            if (specifications.Criteria is not null) { 
                Query=Query.Where(specifications.Criteria);
            }

            Query = specifications.Includes.Aggregate(Query, (currentQuery, include) => currentQuery.Include(include));
            if (specifications.IsPaginated)
            {
                Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }
    }
}
