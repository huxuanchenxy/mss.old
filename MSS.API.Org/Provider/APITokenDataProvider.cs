using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Web.Org.Provider
{
    public class APITokenDataProvider : IAPITokenDataProvider
    {
        private static readonly HttpClient client;

        public IConfiguration Configuration { get; }

        private readonly IDistributedCache _cache;

        static APITokenDataProvider()
        {
            client = new HttpClient();
        }
        public APITokenDataProvider(IConfiguration configuration, IDistributedCache cache)
        {
            //_logger = logger;
            Configuration = configuration;
            _cache = cache;
        }


        public async Task<string> GetRedisAsync()
        {
            var ret = string.Empty;

            ret =  await _cache.GetStringAsync("mss3");
            //ret =  _cache.Get("mss3");

            return ret;
        }

        
    }


}
