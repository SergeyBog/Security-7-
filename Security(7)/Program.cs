using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security_7_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
             .ConfigureServices((context, services) =>
             {
                 HostConfig.CertPath = context.Configuration["CertPath"];
                 HostConfig.CertPassword = "!!Sergey@@123";
             })
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.ConfigureKestrel(options =>
                     {
                         options.ListenAnyIP(5000);
                         options.ListenAnyIP(443, listOpt =>
                         {
                             listOpt.UseHttps(HostConfig.CertPath, HostConfig.CertPassword);
                         });
                     });
                     webBuilder.UseStartup<Startup>();
                 });
    }
    public static class HostConfig
    {
        public static string CertPath { get; set; }
        public static string CertPassword { get; set; }
    }
}
