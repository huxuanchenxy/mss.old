using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRedis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MSS.Common.Consul;
using MSS.Common.Consul.Controller;
using MSS.Web.Org.Provider;

namespace MSS.API.Org.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private readonly IServiceDiscoveryProvider ConsulServiceProvider;
        private readonly IAPITokenDataProvider _APITokenDataProvider;

        private readonly IRedisMQ _redis;
        public ValuesController(IServiceDiscoveryProvider consulServiceProvider, IAPITokenDataProvider APITokenDataProvider, IRedisMQ redis)
        {
            ConsulServiceProvider = consulServiceProvider;
            _APITokenDataProvider = APITokenDataProvider;
            _redis = redis;
        }

        [HttpGet, Route("GetUserInfo")]
        public ActionResult<IEnumerable<string>> GetUserInfo()
        {
            var ret = "ServiceB" + HttpContext.Request.Host.Port + " " + DateTime.Now.ToString();

            return new string[] { ret };
        }

        [HttpGet, Route("GetConsul")]
        public async Task<IActionResult> GetConsul()
        {
            var _services = await ConsulServiceProvider.GetServicesAsync("ServiceA");
            //string api_key = Constants.Redis_API_Key;
            //var token = await _cache.GetStringAsync(api_key);
            return new JsonResult(_services);
            //return View();
        }

        [HttpGet, Route("GetConsul2")]
        public async Task<IActionResult> GetConsul2()
        {
            //var _services = await ConsulServiceProvider.GetServiceAsync("ServiceA");
            //string api_key = Constants.Redis_API_Key;
            //var token = await _cache.GetStringAsync(api_key);
            var csredis = new CSRedis.CSRedisClient("10.89.36.204:6379,password=Test01supersecret,defaultDatabase=7,poolsize=50,ssl=false,writeBuffer=10240");

            //RedisHelper.Initialization(_redis.ConnectCSRedis);
            //var temp = _redis.Get("test1");
            //_redis.Set("test4", "test4name");
            var _services = await _APITokenDataProvider.GetRedisAsync();
            return new JsonResult(_services);
            //return View();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
