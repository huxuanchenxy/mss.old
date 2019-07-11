using System;
using System.Net;
using System.Threading;
using Thrift;
using Thrift.Protocols;
using Thrift.Transports;
using Thrift.Transports.Client;

namespace MSS.RPC.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            RunClient();

            Console.Read();

        }

        static async void RunClient()
        {
            try
            {
                CancellationToken token = new CancellationToken();
                TClientTransport clientTransport = new TSocketClientTransport(IPAddress.Parse("10.89.36.187"), 9090);
                TProtocol protocol = new TBinaryProtocol(clientTransport);
                ThriftCore.Calculator.Client client = new ThriftCore.Calculator.Client(protocol);

                Console.WriteLine("客户端开始连接服务器9090端口");
                await client.OpenTransportAsync(token);

                try
                {
                    Console.WriteLine("连接服务器成功");
                    await client.pingAsync(token);
                    Console.WriteLine("ping()");

                    int sum = client.addAsync(12, 33, token).Result;
                    Console.WriteLine("12+33={0}", sum);
                }
                finally
                {
                    Console.WriteLine("客户端关闭");
                    clientTransport.Close();
                }
            }
            catch (TApplicationException x)
            {
                Console.WriteLine(x.StackTrace);
            }
        }
    }
}
