using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            //the above line is that once we are done with this scope 
            //the scope would be disposed 
            var services = scope.ServiceProvider;
            try
            {
                //we are using the service locator pattern 
                //so that we can get the DataConetext and use it in our local variable

                var context = services.GetRequiredService<DataContext>();
                context.Database.Migrate();

            }
            catch (System.Exception ex)
            {

                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during Migration");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
