using BookReview.Data.DTO.AuthenticationDTO;
using BookReview.Services;
using BookReview.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace BookReview.Controllers
{
    public class AuthenticationController(IAuthenticationService authenticationService,UserManager<IdentityUser> _userManager):ControllerBase
    {

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest request) => Ok(await authenticationService.LoginAsync(request));

        [HttpPost("register")] 
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest request) => Ok(await authenticationService.RegisterAsync(request));

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(string email) => Ok(await authenticationService.GetUserByEmail(email));
        // ✅ Step 1: Forgot Password (send email)
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string Email, [FromServices] IEmailSender _emailSender, [FromServices] IConfiguration _config)
        {
            if (string.IsNullOrWhiteSpace(Email))
                return BadRequest(new { message = "Email is required." });

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                // Do not reveal whether the user exists
                return Ok(new { message = "If that account exists, a password reset email has been sent." });
            }


            await authenticationService.ForgetPassword(Email);
            return Ok(new { message = "If that account exists, a password reset email has been sent." });
        }

        // ✅ Step 2: Reset Password (apply new password)
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Ok(new { message = "If that account exists, password reset has been processed." });
            }

            // decode token
            string decodedToken;
            try
            {
                var tokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                decodedToken = Encoding.UTF8.GetString(tokenBytes);
            }
            catch
            {
                return BadRequest(new { message = "Invalid or corrupted token." });
            }

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);

            if (result.Succeeded)
                return Ok(new { message = "Password reset successful." });

            return BadRequest(new
            {
                message = "Password reset failed.",
                errors = result.Errors.Select(e => e.Description)
            });
        }
    }
}

