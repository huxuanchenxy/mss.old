using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MSS.Common.Consul;

namespace MSS.API.Org.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public IConfiguration Configuration { get; }
        public ValuesController(IConfiguration configuration)
        {
            Configuration = configuration;
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
            string url = "http://" + Configuration["Consul:IP"] + ":" + Configuration["Consul:Port"];
            var _services = await new ConsulServiceProvider().GetServicesAsync(url);
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
