
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Repositories;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configure Services

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDbInitlizer, DbInitlizer>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

            #endregion

            var app = builder.Build();
           await InitializeDbAsync(app);

            #region Configure Ketrel MiddleWares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
  
             async Task InitializeDbAsync (WebApplication app)
            {
                // Create Object FromType That implements IDbInitlizer
                using var scope = app.Services.CreateScope();
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitlizer>();

                await dbInitializer.InitializeAsync();
            }
        }
    }
}
