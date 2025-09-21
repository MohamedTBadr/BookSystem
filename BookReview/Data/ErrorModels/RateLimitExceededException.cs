using Azure.Core;

namespace BookReview.Data.ErrorModels
{
    public class RateLimitExceededException(string message= "Too many requests, please slow down.") : Exception(message)
    {
        
    }

}
