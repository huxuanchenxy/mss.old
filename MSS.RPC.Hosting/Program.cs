using System;
using System.Threading;
using Thrift.Protocols;
using Thrift.Server;
using Thrift.Transports;
using Thrift.Transports.Server;
using ThriftCore;

namespace MSS.RPC.Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationToken token = new CancellationToken();
            TServerTransport serverTransport = new TServerSocketTransport(9090);
            TBinaryProtocol.Factory factory1 = new TBinaryProtocol.Factory();
            TBinaryProtocol.Factory factory2 = new TBinaryProtocol.Factory();

            Calculator.AsyncProcessor processor = new Calculator.AsyncProcessor(new Server());

            TBaseServer server = new AsyncBaseServer(processor, serverTransport, factory1, factory2, new Microsoft.Extensions.Logging.LoggerFactory());

            //server.Serve();
            server.ServeAsync(token);

            Console.WriteLine("服务器正在监听9090端口");
            Console.Read();
        }
    }
}
