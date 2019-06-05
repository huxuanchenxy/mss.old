using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.PlatformAbstractions;
using MSS.API.Dao;

namespace MSS.API.Ids
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(); 
            services.AddDapper(Configuration);

            //string basePath = PlatformServices.Default.Application.ApplicationBasePath;

            services.AddIdentityServer()
                //.AddSigningCredential(new X509Certificate2(Path.Combine(basePath,
                //Configuration["Certificates:CerPath"]),
                //Configuration["Certificates:Password"]))
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddDeveloperSigningCredential()
             .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryClients(Config.GetClients())
             //.AddTestUsers(Config.GetTestUser())
             .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
             //   .AddProfileService<ProfileService>()
            ;


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            //app.UseMvcWithDefaultRoute();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
        }
    }
}
