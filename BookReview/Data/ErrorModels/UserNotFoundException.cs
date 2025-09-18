namespace BookReview.Data.ErrorModels
{
    public class UserNotFoundException(string email): NotFoundException($"User with email {email} not found")
    {
    }
}
