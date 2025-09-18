using BookReview.Data.DTO.AuthenticationDTO;

namespace BookReview.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<UserResponse> GetUserByEmail(string email);
        Task<bool> CheckEmailAsync(string email);
        Task ForgetPassword(string email);
    }
}
