namespace BookReview.Data.ErrorModels
{
    public class BookNotFoundException(Guid id) : NotFoundException($"Book {id} Not Found ")
    {
    }
}
