namespace BookReview.Services.Common
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        private ServiceResult(bool isSuccess,T? data, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }
        public static ServiceResult<T> Success(T? data, string message = null)
      => new ServiceResult<T>(true, data, message);
        public static ServiceResult<T> Failure(string message)
            => new ServiceResult<T>(false, default, message);
    }
}
