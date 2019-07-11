using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace MSS.RPC.Client.ServiceC
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TTransport transport = new TSocket("10.89.36.59", 9001))
            {
                using (TProtocol protocol = new TBinaryProtocol(transport))
                {
                    using (var clientUser = new UserService.Client(protocol))
                    {
                        transport.Open();
                        User u = clientUser.Get(1);
                        Console.WriteLine($"{u.Id},{u.Name}");
                    }
                }
            }
        }
    }
}
