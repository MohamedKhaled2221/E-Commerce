
using System.Reflection.Metadata;
using Domain.Contracts;
using E_Commerce.Extensions;
using E_Commerce.Factories;
using E_Commerce.MiddleWares;
using Microsoft.AspNetCore.Mvc;
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
            #region Part 6 Clean Program Service Extensions

            // Add services to the container.

            //WebbAPI Services
            builder.Services.AddWebApiServices();


            // Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // Core Services
            builder.Services.AddCoreServices(builder.Configuration);



            #endregion

            var app = builder.Build();


            #region Configure Ketrel MiddleWares
            app.UseCustomExceptionMiddleWares();

            await app.SeedDbAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            #endregion
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
  
           
        }
    }
}
