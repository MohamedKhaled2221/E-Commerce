
using Services;
using Services.Abstraction.Contracts;
using Services.Implementations;

namespace E_Commerce.Extensions
{
    public static class CoreServiceExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
             // Core Services
            services.AddAutoMapper(o => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
