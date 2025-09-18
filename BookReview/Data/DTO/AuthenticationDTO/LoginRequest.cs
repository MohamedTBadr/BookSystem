using System.ComponentModel.DataAnnotations;

namespace BookReview.Data.DTO.AuthenticationDTO
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string  Password{ get; set; }
    }
}
