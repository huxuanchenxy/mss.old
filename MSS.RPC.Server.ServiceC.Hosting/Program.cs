using Consul;
using MSS.RPC.Server.ServiceC.Impl;
using System;
using System.Threading;
using Thrift.Server;
using Thrift.Transport;

namespace MSS.RPC.Server.ServiceC.Hosting
{
    class Program
    {
        static  void Main(string[] args)
        {
            Console.WriteLine("用户中心RPC Server已启动...");
            TServerTransport transport = new TServerSocket(9001);
            var processor = new UserService.Processor(new UserServiceImpl());
            TServer server = new TThreadPoolServer(processor, transport);

            //如果多个服务实现的话，也可以这样启动；
            //var processor2 = new Manulife.DNC.MSAD.Contracts.PayoutService.Processor(new PayoutServiceImpl());
            //var processorMulti = new Thrift.Protocol.TMultiplexedProcessor();
            //processorMulti.RegisterProcessor("Service1", processor1);
            //processorMulti.RegisterProcessor("Service2", processor2);
            //TServer server = new TThreadedServer(processorMulti, transport);
            // lanuch
            RegisterConsul();
            server.Serve();

            
        }

        private static void RegisterConsul()
        {
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://10.89.36.59:8500"));//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                //HTTP = $"http://{healthService.Value.IP}:{healthService.Value.Port}/health",//健康检查地址
                TCP = "10.89.36.187:9001",
                Timeout = TimeSpan.FromSeconds(5)
            };

            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = "RPCSericeC" + "_" + "9001",
                Name = "RPCSericeC",
                Address = "10.89.36.187",
                Port = 9001,
                Tags = new[] { $"urlprefix-/RPCSericeC" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            //lifetime.ApplicationStopping.Register(() =>
            //{
            //    consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            //});
        }
    }
}
