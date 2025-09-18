namespace BookReview.Data.DTO
{
    public record PagedResult<T> 
    (
        IEnumerable<T> Data
       ,
         int TotalCount,
         int PageNumber ,
         int PageSize 
    );
}
