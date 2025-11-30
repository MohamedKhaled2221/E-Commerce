
using Services;
using Services.Abstraction.Contracts;
using Services.Implementations;
using Shared;
using ProductService = Services.Implementations.ProductService;
namespace E_Commerce.Extensions
{
    public static class CoreServiceExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
             // Core Services
            services.AddAutoMapper(o => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICacheService, CacheService>();


            services.AddScoped<Func<IProductService>>(provider =>
             () => provider.GetRequiredService<IProductService>());
            services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>());
            services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>());
            services.AddScoped<Func<IPaymentService>>(provider =>
            () => provider.GetRequiredService<IPaymentService>());
            services.AddScoped<Func<IOrderService>>(provider =>
            () => provider.GetRequiredService<IOrderService>());
            services.AddScoped<Func<ICacheService>>(provider =>
            () => provider.GetRequiredService<ICacheService>());

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
