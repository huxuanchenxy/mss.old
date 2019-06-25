using CSRedis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSS.Common.Web
{
    public class GlobalActionFilter: IActionFilter
    {
        private  IConfiguration Configuration { get; }
        public GlobalActionFilter(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers["Access-Control-Allow-Origin"] = "*";//解决拦截器添加后跨域不生效的问题
            //if (!context.ModelState.IsValid)//验证参数的合法性问题。返回错误信息
            //{
            ///模型有效性验证失败处理逻辑...比如将提示信息返回


            //context.Result = new ContentResult
            //{
            //    Content = stringBuilder.ToString(),
            //    StatusCode = StatusCodes.Status200OK,
            //    ContentType = "text/html;charset=utf-8"
            //};
            //}
            if (context.HttpContext.Response.StatusCode == StatusCodes.Status200OK)
            {
                //记日志
                var controllerName = context.RouteData.Values["Controller"].ToString();
                var actionName = context.RouteData.Values["Action"].ToString();
                var methodName = context.HttpContext.Request.Method.ToString();
                var head = context.HttpContext.Request.Headers["Authorization"].ToString();
                var token = string.Empty;
                if (head.IndexOf("Bearer") >= 0)
                {
                    token = head.Replace("Bearer", "").Trim();
                }

                string userid = string.Empty;
                using (var csredis = new CSRedisClient(Configuration["redis:ConnectionString"]))
                {
                    userid = csredis.Get(token);
                }
                    

                

            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

    }
}
