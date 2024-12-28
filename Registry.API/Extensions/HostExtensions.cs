using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace Registry.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host,
            Action<TContext, IServiceProvider> seeder,
            int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                
                try
                {
                    logger.LogInformation("Migrating PostgreSQL database started.");
                    InvokeSeeder(seeder, context, services);
                    
                    
                    
                    logger.LogInformation("PostgreSQL database migration completed.");
                }
                catch (PostgresException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the PostgreSQL database.");
                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                    throw;
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(
            Action<TContext, IServiceProvider> seeder,
            TContext context,
            IServiceProvider services)
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}