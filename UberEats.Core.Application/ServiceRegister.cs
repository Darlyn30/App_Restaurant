﻿
using Microsoft.Extensions.DependencyInjection;
using UberEats.Core.Application.Helpers;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.Services;

namespace UberEats.Core.Application
{
    public static class ServiceRegister
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IVerifyAccountService, VerifyAccountService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IFoodService, FoodService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IHistoryService, HistoryService>();
            #endregion
        }
    }
}
