using BookReview.Data.ErrorModels;
using System.Net;
using System.Text.Json;

namespace BookReview.Middlewares
{
    public class CustomExceptionHandlerMiddleware
        (RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        //response shape [status code,Error msg]

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                //logging exception
                    logger.LogError($"Something went wrong:{ex.Message}");
                //handling it
                await HandleExceptionAsync(context,ex);
            } 
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            //set content type -> app/json
            context.Response.ContentType = "application/json";

            //set status code to 500 if internal,etc
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                RateLimitExceededException=>(int)HttpStatusCode.TooManyRequests,
                _ =>(int)HttpStatusCode.InternalServerError
            };

            //return standard response 
            var response = new ErrorDetails//C# object 
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            var response2 = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(response2);

        }
    }
}
