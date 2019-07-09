using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrift.Protocols;
using Thrift.Server;
using Thrift.Transports;
using Thrift.Transports.Server;
using ThriftCore;

namespace MSS.RPC.Server
{
    public class Server : ThriftCore.Calculator.IAsync
    {
        public Task<int> addAsync(int num1, int num2, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                int result = num1 + num2;
                Console.WriteLine("服务端计算结果" + result);
                return result;
            });
        }


        public Task pingAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("服务端收到Ping指令");
            });
        }
    }

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
