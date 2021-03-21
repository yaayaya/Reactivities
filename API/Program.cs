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
        // Async Version
        public static async Task Main(string[] args)
        {
            // 建立Host
            var host = CreateHostBuilder(args).Build();
            
            /// 建立拋棄式scope service
            var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            // 取得DataContext 建立Migration
            try
            {
                var context = services.GetRequiredService<DataContext>();
                // 改為Migrate Async
                await context.Database.MigrateAsync();
                // 檢查Table預設資料
                await Seed.SeedData(context);
            }
            catch(Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex , "An error occured during migration");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
