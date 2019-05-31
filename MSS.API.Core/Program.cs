﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace MSS.API.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>

              WebHost.CreateDefaultBuilder(args)

                .UseContentRoot(Directory.GetCurrentDirectory())
                 .UseIISIntegration()

                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 3851);
                    //options.Listen(IPAddress.Any, 443, listenOptions =>
                    //{
                    //    listenOptions.UseHttps("server.pfx", "password");
                    //});
                })
              .UseStartup<Startup>()
                .Build();
    }
}
