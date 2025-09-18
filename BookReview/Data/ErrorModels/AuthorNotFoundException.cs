namespace BookReview.Data.ErrorModels
{
    public class AuthorNotFoundException(Guid id):NotFoundException($"Author With Id: {id} Not Found");
    
    
}
