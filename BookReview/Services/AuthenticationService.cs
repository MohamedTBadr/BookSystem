using AutoMapper;
using BookReview.Data.DTO.AuthenticationDTO;
using BookReview.Data.ErrorModels;
using BookReview.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookReview.Services
{
    public class AuthenticationService(UserManager<IdentityUser> _userManager, IOptions<JWTOptions> options, IMapper mapper,IConfiguration configuration,IEmailSender _emailSender) 
        : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        => (await _userManager.FindByEmailAsync(email)) != null;

      

        public async Task<UserResponse> GetUserByEmail(string email)
        {


            var User = await _userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException(email);


            return new(email, User.PasswordHash, await GenerateTokenAsync(User));
        }
       
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            //check If User Exist
            var User = await _userManager.FindByEmailAsync(request.Email) ?? throw new UserNotFoundException(request.Email);


            var Isvalid = await _userManager.CheckPasswordAsync(User, request.Password);
            if (Isvalid) return new(request.Email, request.Password,await GenerateTokenAsync(User));

            throw new UnauthorizedException();
            

        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var User = new IdentityUser
            {
                Email = request.Email,
                UserName=request.UserName
            };

            var Result = await _userManager.CreateAsync(User, request.Password);
            if (Result.Succeeded) return new(request.Email, request.Password, await GenerateTokenAsync(User));
            var errors = Result.Errors.Select(e => e.Description).ToList();
            throw new BadRequestException(errors);



        }

        public async Task  ForgetPassword(string email)
        {
            var user=await _userManager.FindByEmailAsync(email)??throw new UserNotFoundException(email);


            // 1) create token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // 2) encode token so it's safe in a URL
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

            // 3) Build callback URL
           
            var baseUrl = configuration.GetSection("clientBaseUrl").Value;
            // Example redirect to front-end page: /account/reset-password (or to MVC action)
            var callbackUrl = $"{baseUrl}Authentication/reset-password?email={Uri.EscapeDataString(user.Email)}&token={encodedToken}";

            // 4) Compose email (HTML)
            var subject = "Reset your password";
            var body = $@"
<table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:#f4f4f4;padding:20px 0;'>
  <tr>
    <td align='center'>
      <table width='600' cellpadding='0' cellspacing='0' border='0' style='background-color:#ffffff;border-radius:8px;padding:20px;font-family:Arial,sans-serif;color:#333333;'>
        <tr>
          <td align='center' style='padding:20px 0;'>
            <h2 style='margin:0;color:#4CAF50;'>Reset Your Password</h2>
          </td>
        </tr>
        <tr>
          <td style='padding:20px;font-size:15px;line-height:1.6;'>
            <p>Hi,</p>
            <p>We received a request to reset your password. Click the button below to continue. 
               This link will expire after a short time for security reasons.</p>
            <p style='text-align:center;margin:30px 0;'>
              <a href='{callbackUrl}' 
                 style='display:inline-block;padding:12px 24px;background-color:#4CAF50;
                        color:#ffffff;text-decoration:none;border-radius:6px;font-weight:bold;'>
                 Reset Password
              </a>
            </p>
            <p>If you didn’t request this, you can safely ignore this email.</p>
            <p style='margin-top:30px;font-size:13px;color:#777;'>Thank you,<br/>The Support Team</p>
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>";


            // 5) send email (use your IEmailSender implementation)
            await _emailSender.SendEmailAsync(user.Email, subject, body);
            

        }
        private async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            var jwt = options.Value;
            var Claims = new List<Claim>()
                {
                new(ClaimTypes.Email,user.Email!),
                new(ClaimTypes.Name,user.UserName!),

                };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var item in Roles)
            {
                Claims.Add(new(ClaimTypes.Role, item));

            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));


            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: Claims,
                expires: DateTime.UtcNow.AddDays(jwt.DurationDays),
                signingCredentials: cred
                );
            var TokenHandler = new JwtSecurityTokenHandler();
            return TokenHandler.WriteToken(token);

        }

    }
}
