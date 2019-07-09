using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MSS.RPC.Server.ServiceA.Entity;
using MSS.RPC.Server.ServiceA.Impl.Entity;
using MSS.RPC.Server.ServiceA.Impl.RpcService;
using Serilog;
using Serilog.Events;
using Thrift;
using Thrift.Transports.Server;
using ThriftCore;

namespace MSS.RPC.Server.ServiceA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var logger = new LoggerConfiguration()

.MinimumLevel.Debug()

        //.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.RollingFile(@"Logs\Info-{Date}.log"))
        .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo.RollingFile(@"Logs/Debug-{Date}.log"))

        .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo.RollingFile(@"Logs/Error-{Date}.log"))
//  .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal).WriteTo.RollingFile(@"Logs\Fatal-{Date}.log"))

.CreateLogger();
            Log.Logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注册全局配置
            //services.AddOptions();
            //services.Configure<GrpcService>(Configuration.GetSection(nameof(GrpcService)));
            //services.Configure<HealthService>(Configuration.GetSection(nameof(HealthService)));
            //services.Configure<ConsulService>(Configuration.GetSection(nameof(ConsulService)));

            ////注册Rpc服务
            //services.AddSingleton<IRpcConfig, RpcConfig>();
            services.AddTransient<Calculator.IAsync, CalculatorAsyncHandler>();
            services.AddTransient<ITAsyncProcessor, Calculator.AsyncProcessor>();
            services.AddTransient<THttpServerTransport, THttpServerTransport>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IOptions<HealthService> healthService, IOptions<ConsulService> consulService, IRpcConfig rpc)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<THttpServerTransport>();
            //// 添加健康检查路由地址
            //app.Map("/health", HealthMap);

            //// 服务注册
            //app.RegisterConsul(lifetime, healthService, consulService);

            //// 启动Rpc服务
            //rpc.Start(int.Parse(Configuration["GrpcService:Port"]));
        }

        private static void HealthMap(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("OK");
            });
        }
    }
}
