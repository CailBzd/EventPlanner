using EventPlanner.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static async Task Main(string[] aArgs)
    {
         var vHost = CreateHostBuilder(aArgs).Build();

        using (var scope = vHost.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var vContext = services.GetRequiredService<ApplicationDbContext>();
                var vUserManager = services.GetRequiredService<UserManager<User>>();
                var vRoleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                // Apply migrations
                vContext.Database.Migrate();

                // Create default user
                await SeedData.Initialize(services, vUserManager, vRoleManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        await vHost.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] aArgs) =>
        Host.CreateDefaultBuilder(aArgs)
        .ConfigureAppConfiguration((context, config) =>
            {
                var vEnv = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{vEnv.EnvironmentName}.json", optional: true, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
