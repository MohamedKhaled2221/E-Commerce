using Domain.Contracts;
using E_Commerce.MiddleWares;

namespace E_Commerce.Extensions
{
    public static class WebApplicationExtensions
    {
     public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            // Create Object FromType That implements IDbInitlizer
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitlizer>();

            await dbInitializer.InitializeAsync();
            return app;
        }
        public static WebApplication UseCustomExceptionMiddleWares(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandelingMiddleware>();
            return app;
        }
        public static WebApplication UseSwaggerMiddleWares(this WebApplication app)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
