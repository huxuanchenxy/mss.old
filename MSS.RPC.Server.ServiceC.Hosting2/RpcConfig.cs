//using Grpc.Core;
//using Snai.GrpcService.Impl.RpcService;
//using Snai.GrpcService.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Options;
using MSS.RPC.Server.ServiceC.Impl;
using Thrift.Server;
using Thrift.Transport;

namespace MSS.RPC.Server.ServiceC.Hosting2
{
    public class RpcConfig: IRpcConfig
    {
        //private static Server _server;
        //static IOptions<Entity.GrpcService> GrpcSettings;

        //public RpcConfig(IOptions<Entity.GrpcService> grpcSettings)
        //{
        //    GrpcSettings = grpcSettings;
        //}

        public void Start(int port)
        {
            //Console.WriteLine("用户中心RPC Server已启动...");
            TServerTransport transport = new TServerSocket(port);
            var processor = new UserService.Processor(new UserServiceImpl());
            TServer server = new TThreadPoolServer(processor, transport);

            //如果多个服务实现的话，也可以这样启动；
            //var processor2 = new Manulife.DNC.MSAD.Contracts.PayoutService.Processor(new PayoutServiceImpl());
            //var processorMulti = new Thrift.Protocol.TMultiplexedProcessor();
            //processorMulti.RegisterProcessor("Service1", processor1);
            //processorMulti.RegisterProcessor("Service2", processor2);
            //TServer server = new TThreadedServer(processorMulti, transport);
            // lanuch
            server.Serve();
            Console.WriteLine("服务器正在监听"+ port + "端口");
        }
    }
}
