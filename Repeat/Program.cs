using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repeat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repeat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            /*When you start app for the first time you need to comment section 
             
             using (var scope = host.Services.CreateScope())
            {
                await DbInitializer.Initialize(scope.ServiceProvider);
            }


            and apply migrations then uncomment previous section for login purpose for admin with 
            admin credentials stored in appsettings.json ("Email": "admin@vsite.hr", "Password": "Admin_2021")*/

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                await DbInitializer.Initialize(scope.ServiceProvider);
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
