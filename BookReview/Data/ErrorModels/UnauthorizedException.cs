namespace BookReview.Data.ErrorModels
{
    public class UnauthorizedException(string msg=null):Exception("UnAuthorized Access")
    {
    }
}
