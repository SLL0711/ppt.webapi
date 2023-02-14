using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace wt.basic.webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseKestrel(options =>
                    {
                        options.Limits.MaxRequestBodySize = 209715200;//限制最大request body不超过200MB
                    });
                })
                .ConfigureLogging(logbuilder =>
                {
                    logbuilder.ClearProviders();
                    logbuilder.AddConsole();
                    logbuilder.AddLog4Net(Path.Combine(Directory.GetCurrentDirectory(), "log4net.config"));
                });
    }
}
