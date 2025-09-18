using BookReview.Data.DTO.AuthenticationDTO;
using BookReview.Data.Repositories.Interfaces;
using BookReview.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace BookReview.Data.Repositories
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection RepositoriesRegistration(IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

           services.AddDbContext<BookDbContext>(options =>
            {
                //var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
 );


          
            services.AddSingleton<IConnectionMultiplexer>(s => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));


            return services;
        }
    }
}
