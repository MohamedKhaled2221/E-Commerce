
using System.Reflection.Metadata;
using Domain.Contracts;
using E_Commerce.MiddleWares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Repositories;
using Services.Abstraction.Contracts;
using Services.Implementations;
using Services.MappingProfile;

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
            builder.Services.AddAutoMapper(o => { }, typeof(Services.AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

             
            #endregion

            var app = builder.Build();
           await InitializeDbAsync(app);

            #region Configure Ketrel MiddleWares
            app.UseMiddleware<GlobalErrorHandelingMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
