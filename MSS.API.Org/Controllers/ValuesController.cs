using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CSRedis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MSS.Common.Consul;
using MSS.Common.Consul.Controller;
using MSS.Web.Org.Provider;
using Newtonsoft.Json;

namespace MSS.API.Org.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private readonly IServiceDiscoveryProvider ConsulServiceProvider;
        private readonly IAPITokenDataProvider _APITokenDataProvider;

        public ValuesController(IServiceDiscoveryProvider consulServiceProvider, IAPITokenDataProvider APITokenDataProvider)
        {
            ConsulServiceProvider = consulServiceProvider;
            _APITokenDataProvider = APITokenDataProvider;
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


        [HttpGet, Route("GetLog")]
        public async Task<ActionResult> GetLog()
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Dictionary<string, string> dic = new Dictionary<string, string>();
                //dic.Add("controller_name", "test111");
                //dic.Add("action_name", "test111");
                //dic.Add("method_name", "test111");
                //dic.Add("acc_name", "test111");
                //dic.Add("user_name", "test111");
                //dic.Add("ip", "test111");
                //dic.Add("mac_add", "test111");

                UserOperationLog parmobj = new UserOperationLog() {controller_name= "test222" ,
                 action_name = "33333",
                method_name = "post",
                acc_name = "tets2",
                user_name = "ddd",
                ip = "33223",
                mac_add = "2332"};
                var content = new StringContent(JsonConvert.SerializeObject(parmobj), Encoding.UTF8, "application/json");
                string httpurl = "http://10.89.36.204:8003/api/v1/UserOperationLog/Add";
                var repes = await client.PostAsync(httpurl, content);
            }
            return Ok("ok");
        }

        public class UserOperationLog 
        {
            public string controller_name { get; set; }
            public string action_name { get; set; }

            public string method_name { get; set; }

            public string acc_name { get; set; }

            public string user_name { get; set; }

            public string ip { get; set; }

            public string mac_add { get; set; }
            public int id { get; set; }
            public DateTime created_time { get; set; }
            public int created_by { get; set; }
            public DateTime updated_time { get; set; }
            public int updated_by { get; set; }
            public int is_del { get; set; }

        }

        // POST api/values
        [HttpPost, Route("test2")]
        public void test2()
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
