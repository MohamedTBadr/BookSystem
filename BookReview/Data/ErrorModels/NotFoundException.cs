namespace BookReview.Data.ErrorModels
{
    public class NotFoundException(string msg):Exception(msg); 
}
