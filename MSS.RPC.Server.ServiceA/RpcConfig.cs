//using Grpc.Core;
//using Snai.GrpcService.Impl.RpcService;
//using Snai.GrpcService.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Options;
using MSS.RPC.Server.ServiceA.Impl.RpcService;
using Thrift.Protocols;
using Thrift.Server;
using Thrift.Transports;
using Thrift.Transports.Server;
using ThriftCore;

namespace MSS.RPC.Server.ServiceA
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
            //CancellationToken token = new CancellationToken();
            //TServerTransport serverTransport = new TServerSocketTransport(port);
            //TBinaryProtocol.Factory factory1 = new TBinaryProtocol.Factory();
            //TBinaryProtocol.Factory factory2 = new TBinaryProtocol.Factory();

            //Calculator.AsyncProcessor processor = new Calculator.AsyncProcessor(new ServiceAImpl());

            //TBaseServer server = new AsyncBaseServer(processor, serverTransport, factory1, factory2, new Microsoft.Extensions.Logging.LoggerFactory());

            ////server.Serve();
            //server.ServeAsync(token);


            //TServerTransport transport = new TServerSocketTransport(port);//监听8800端口
            //var processor = new Calculator.AsyncProcessor(new ServiceAImpl());
            //TBaseServer server = new TThreadPoolServer(processor, transport);
            //server.Serve();
            Console.WriteLine("服务器正在监听"+ port + "端口");
        }
    }
}
