using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MSS.Common.Consul;
using MSS.Common.Consul.Controller;

namespace MSS.API.Org.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private readonly IServiceDiscoveryProvider ConsulServiceProvider;
        public ValuesController(IServiceDiscoveryProvider consulServiceProvider)
        {
            ConsulServiceProvider = consulServiceProvider;
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
            var _services = await ConsulServiceProvider.GetServiceAsync("ServiceA");
            //string api_key = Constants.Redis_API_Key;
            //var token = await _cache.GetStringAsync(api_key);
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
