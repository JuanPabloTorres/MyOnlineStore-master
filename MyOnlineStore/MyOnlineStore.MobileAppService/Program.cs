using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using System.Collections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MyOnlineStore.MobileAppService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var keys = new ArrayList(Environment.GetEnvironmentVariables().Keys);
            var values = new ArrayList(Environment.GetEnvironmentVariables().Values);
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < keys.Count; i++)
            {
                Console.WriteLine($"Key: {keys[i]} Value: {values[i]}");
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(o => o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2.5));
                webBuilder.UseUrls("http://0.0.0.0:5000");
                webBuilder.UseStartup<Startup>();
            });
    }
}