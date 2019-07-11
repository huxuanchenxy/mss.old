using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MSS.RPC.Server.ServiceA.Impl.RpcService;
using Thrift;
using Thrift.Protocols;
using Thrift.Server;
using Thrift.Transports;
using Thrift.Transports.Server;
using ThriftCore;

namespace MSS.RPC.Server.ServiceA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var source = new CancellationTokenSource())
            {
                RunAsync(args, source.Token).GetAwaiter().GetResult();

                //Logger.LogInformation("Press any key to stop...");

                //Console.ReadLine();
                //source.Cancel();
            }
            //BuildWebHost(args).Run();
            var host = BuildWebHost(args);
            //host.RunAsync(CancellationToken).GetAwaiter().GetResult();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            int port = int.Parse(args[0]);//3861
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel()
                    .UseUrls("http://localhost:" + port)
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>()
                    .Build();

        }

        private static async Task RunAsync(string[] args, CancellationToken cancellationToken)
        {
            var selectedTransport = GetTransport(args);
            var selectedProtocol = GetProtocol(args);

            //if (selectedTransport == Transport.Http)
            //{
            //    new HttpServerSample().Run(cancellationToken);
            //}
            //else
            //{
            //    await RunSelectedConfigurationAsync(selectedTransport, selectedProtocol, cancellationToken);
            //}
        }

        private static Protocol GetProtocol(string[] args)
        {
            var transport = args.FirstOrDefault(x => x.StartsWith("-pr"))?.Split(':')?[1];

            Enum.TryParse(transport, true, out Protocol selectedProtocol);

            return selectedProtocol;
        }

        private static Transport GetTransport(string[] args)
        {
            var transport = args.FirstOrDefault(x => x.StartsWith("-tr"))?.Split(':')?[1];

            Enum.TryParse(transport, true, out Transport selectedTransport);

            return selectedTransport;
        }

        private static async Task RunSelectedConfigurationAsync(Transport transport, Protocol protocol, CancellationToken cancellationToken)
        {
            var fabric = new LoggerFactory().AddConsole(LogLevel.Trace).AddDebug(LogLevel.Trace);
            var handler = new CalculatorAsyncHandler();
            ITAsyncProcessor processor = null;

            TServerTransport serverTransport = null;

            switch (transport)
            {
                case Transport.Tcp:
                    serverTransport = new TServerSocketTransport(9090);
                    break;
                case Transport.TcpBuffered:
                    serverTransport = new TServerSocketTransport(port: 9090, clientTimeout: 10000, useBufferedSockets: true);
                    break;
                case Transport.NamedPipe:
                    serverTransport = new TNamedPipeServerTransport(".test");
                    break;
                case Transport.TcpTls:
                    serverTransport = new TTlsServerSocketTransport(9090, false, GetCertificate(), ClientCertValidator, LocalCertificateSelectionCallback);
                    break;
                case Transport.Framed:
                    serverTransport = new TServerFramedTransport(9090);
                    break;
            }

            ITProtocolFactory inputProtocolFactory;
            ITProtocolFactory outputProtocolFactory;

            switch (protocol)
            {
                case Protocol.Binary:
                    {
                        inputProtocolFactory = new TBinaryProtocol.Factory();
                        outputProtocolFactory = new TBinaryProtocol.Factory();
                        processor = new Calculator.AsyncProcessor(handler);
                    }
                    break;
                case Protocol.Compact:
                    {
                        inputProtocolFactory = new TCompactProtocol.Factory();
                        outputProtocolFactory = new TCompactProtocol.Factory();
                        processor = new Calculator.AsyncProcessor(handler);
                    }
                    break;
                case Protocol.Json:
                    {
                        inputProtocolFactory = new TJsonProtocol.Factory();
                        outputProtocolFactory = new TJsonProtocol.Factory();
                        processor = new Calculator.AsyncProcessor(handler);
                    }
                    break;
                case Protocol.Multiplexed:
                    {
                        inputProtocolFactory = new TBinaryProtocol.Factory();
                        outputProtocolFactory = new TBinaryProtocol.Factory();

                        var calcHandler = new CalculatorAsyncHandler();
                        var calcProcessor = new Calculator.AsyncProcessor(calcHandler);

                        //var sharedServiceHandler = new SharedServiceAsyncHandler();
                        //var sharedServiceProcessor = new SharedService.AsyncProcessor(sharedServiceHandler);

                        var multiplexedProcessor = new TMultiplexedProcessor();
                        multiplexedProcessor.RegisterProcessor(nameof(Calculator), calcProcessor);
                        //multiplexedProcessor.RegisterProcessor(nameof(SharedService), sharedServiceProcessor);

                        processor = multiplexedProcessor;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(protocol), protocol, null);
            }

            try
            {
                //Logger.LogInformation(
                //   $"Selected TAsyncServer with {serverTransport} transport, {processor} processor and {inputProtocolFactory} protocol factories");

                var server = new AsyncBaseServer(processor, serverTransport, inputProtocolFactory, outputProtocolFactory, fabric);

                //Logger.LogInformation("Starting the server...");
                await server.ServeAsync(cancellationToken);
            }
            catch (Exception x)
            {
                //Logger.LogInformation(x.ToString());
            }
        }

        private static X509Certificate2 GetCertificate()
        {
            // due to files location in net core better to take certs from top folder
            var certFile = GetCertPath(Directory.GetParent(Directory.GetCurrentDirectory()));
            return new X509Certificate2(certFile, "ThriftTest");
        }

        private static string GetCertPath(DirectoryInfo di, int maxCount = 6)
        {
            var topDir = di;
            var certFile =
                topDir.EnumerateFiles("ThriftTest.pfx", SearchOption.AllDirectories)
                    .FirstOrDefault();
            if (certFile == null)
            {
                if (maxCount == 0)
                    throw new FileNotFoundException("Cannot find file in directories");
                return GetCertPath(di.Parent, maxCount - 1);
            }

            return certFile.FullName;
        }

        private static X509Certificate LocalCertificateSelectionCallback(object sender,
            string targetHost, X509CertificateCollection localCertificates,
            X509Certificate remoteCertificate, string[] acceptableIssuers)
        {
            return GetCertificate();
        }

        private static bool ClientCertValidator(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private enum Transport
        {
            Tcp,
            TcpBuffered,
            NamedPipe,
            Http,
            TcpTls,
            Framed
        }

        private enum Protocol
        {
            Binary,
            Compact,
            Json,
            Multiplexed
        }




    }
}
