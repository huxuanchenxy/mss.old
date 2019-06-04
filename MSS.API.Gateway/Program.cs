using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MSS.API.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                            .UseStartup<Startup>()
                                          .UseKestrel(options =>
                                          {
                                              options.Listen(IPAddress.Loopback, 5800);
                                              //options.Listen(IPAddress.Any, 443, listenOptions =>
                                              //{
                                              //    listenOptions.UseHttps("server.pfx", "password");
                                              //});
                                          })
                            .ConfigureAppConfiguration((hostingContext, builder) =>
                            {
                                builder.AddJsonFile("configuration.json", false, true);
                            })
                            .Build();
        }
    }
}
