﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MSS.API.Ids
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
              .UseKestrel(options =>
              {
                  options.Listen(IPAddress.Loopback, 5000);
                  //options.Listen(IPAddress.Any, 443, listenOptions =>
                  //{
                  //    listenOptions.UseHttps("server.pfx", "password");
                  //});
              })
                .UseStartup<Startup>()
                .Build();


    }
}
