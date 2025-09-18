
using BookReview.Data.DTO.AuthenticationDTO;
using BookReview.Data.Repositories;
using BookReview.Data.Repositories.Interfaces;
using BookReview.Data.UnitOfWork;
using BookReview.Middlewares;
using BookReview.Services;
using BookReview.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace BookReview
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.



            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<BookDbContext>()
.AddDefaultTokenProviders();

            RepositoryRegistration.RepositoriesRegistration(builder.Services,builder.Configuration);
            ServiceRegistration.ServicesRegistration(builder.Services,builder.Configuration);
            
            
           

 


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
