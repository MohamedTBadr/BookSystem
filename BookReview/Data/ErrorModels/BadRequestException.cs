namespace BookReview.Data.ErrorModels
{
    public class BadRequestException(List<string> errors):Exception("ValidationError")
    {
        public List<string> Errors { get; } = errors;
    }
}
