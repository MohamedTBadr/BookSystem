using BookReview.Data.DTO.AuthenticationDTO;
using BookReview.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace BookReview.Services
{
    public  static class ServiceRegistration
    {

        public static IServiceCollection ServicesRegistration(IServiceCollection services,IConfiguration configuration)
        {
          services.AddScoped<IBookService, BookService>();
          services.AddScoped<ICacheService, CacheService>();
          services.AddScoped<IAuthorService, AuthorService>();
          services.AddAutoMapper(typeof(Services.MappingProfiles).Assembly);
         services.AddScoped<IAuthenticationService, AuthenticationService>();
          services.AddScoped<IEmailSender, EmailSenderService>();
            services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
            return services;
        }
    }
}
