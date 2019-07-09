using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MSS.RPC.Server.ServiceC.Hosting2
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
            //注册Rpc服务
            services.AddSingleton<IRpcConfig, RpcConfig>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime,IRpcConfig rpc)
        {
            //ServiceEntity serviceEntity = new ServiceEntity
            //{
            //    IP = NetworkHelper.LocalIPAddress,
            //    Port = Convert.ToInt32(Configuration["Service:Port"]),
            //    ServiceName = Configuration["Service:Name"],
            //    ConsulIP = Configuration["Consul:IP"],
            //    ConsulPort = Convert.ToInt32(Configuration["Consul:Port"])
            //};
            //app.RegisterConsul(lifetime, serviceEntity);
            rpc.Start(9001);
        }
    }
}
