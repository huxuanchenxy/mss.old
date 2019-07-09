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


namespace MSS.RPC.Server.ServiceA.Impl.RpcService
{
    public class CalculatorAsyncHandler : ThriftCore.Calculator.IAsync
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
}
