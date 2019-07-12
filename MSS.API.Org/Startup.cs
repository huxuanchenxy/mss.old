using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSS.API.Operlog.Common;
using MSS.Common.Consul;
using MSS.Web.Org.Provider;
using System;


namespace MSS.API.Org
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IServiceDiscoveryProvider,ConsulServiceProvider>();
            //services.AddTransient<IActionFilter, GlobalActionFilter>();
            services.AddTransient<IAPITokenDataProvider, APITokenDataProvider>();

            services.AddMvc(
                options =>
                {
                    options.Filters.Add<GlobalActionFilter>();
                }
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            // register this service
            //ServiceEntity serviceEntity = new ServiceEntity
            //{
            //    //IP = NetworkHelper.LocalIPAddress,
            //    Port = Convert.ToInt32(Configuration["Service:Port"]),
            //    ServiceName = Configuration["Service:Name"],
            //    ConsulIP = Configuration["Consul:IP"],
            //    ConsulPort = Convert.ToInt32(Configuration["Consul:Port"])
            //};
            //app.RegisterConsul(lifetime, serviceEntity);
        }
    }
}
