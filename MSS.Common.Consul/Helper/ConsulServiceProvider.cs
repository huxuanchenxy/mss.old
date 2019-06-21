using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Consul;

namespace MSS.Common.Consul
{
    public class ConsulServiceProvider : IServiceDiscoveryProvider
    {
        public async Task<List<string>> GetServicesAsync(string consulurl)
        {
            var consuleClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(consulurl);
            });

            var queryResult = await consuleClient.Health.Service("ServiceA", string.Empty, true);

            while (queryResult.Response.Length == 0)
            {
                Console.WriteLine("No services found, wait 1s....");
                await Task.Delay(1000);
                queryResult = await consuleClient.Health.Service("ServiceA", string.Empty, true);
            }

            var result = new List<string>();
            foreach (var serviceEntry in queryResult.Response)
            {
                result.Add(serviceEntry.Service.Address + ":" + serviceEntry.Service.Port);
            }
            return result;
        }
    }
}
