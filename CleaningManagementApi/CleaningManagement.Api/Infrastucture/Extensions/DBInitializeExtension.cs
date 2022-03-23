using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CleaningManagement.BusinessLogic.Interfaces;
using CleaningManagement.BusinessLogic.Entity;
using CleaningManagement.DAL.Seed;
using System;

namespace CleaningManagement.Api.Infrastucture.Extensions
{
    static class DBInitializeExtension
    {
        public static IWebHost DBInitialize(this IWebHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var repository = services.GetRequiredService<IRepository<CleaningPlan>>();
                var dBinitializer = new DbInitializer(repository);
                dBinitializer.Initialize();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }

            return host;
        }
    }
}