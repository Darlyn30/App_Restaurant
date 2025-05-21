using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Infrastructure.Persistence.Contexts;
using UberEats.Infrastructure.Persistence.Repositories;

namespace UberEats.Infrastructure.Persistence
{
    public static class ServiceRegister
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region contexts
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            #endregion

            #region repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVerifyAccountRepository, VerifyAccountRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            #endregion
        }
    }
}
