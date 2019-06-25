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
        private readonly IDistributedCache Cache;
        public GlobalActionFilter(IConfiguration configuration, IDistributedCache cache)
        {
            Configuration = configuration;
            Cache = cache;
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
                var redisinstance = Configuration["redis:Instance"];
                var rediskey = redisinstance + token;
                if (rediskey == "msseyJhbGciOiJSUzI1NiIsImtpZCI6ImMwZGM0NzZhY2ZjNTY3OGNjNTViYzc0ZmIzOWUwMzA3IiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NjE0NDI5NTMsImV4cCI6MTU2MTQ0NjU1MywiaXNzIjoiaHR0cDovLzEwLjg5LjM2LjIwNDo1MDAwIiwiYXVkIjpbImh0dHA6Ly8xMC44OS4zNi4yMDQ6NTAwMC9yZXNvdXJjZXMiLCJNc3NTZXJ2aWNlIl0sImNsaWVudF9pZCI6InB3ZENsaWVudCIsInN1YiI6IjMiLCJhdXRoX3RpbWUiOjE1NjE0NDI5NTMsImlkcCI6ImxvY2FsIiwic2NvcGUiOlsiTXNzU2VydmljZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJjdXN0b20iXX0.x44sJslNhARwt4V777OzcC-aHVdijQe-k8TZXt7r2e5zhAYQEw-TS5IZGctbonZnPYEWf77USaHVkLAsaAianPTKzUhfuKU9DEBR6us3iIkcn0cHup7aIgamb6NGBwyg3wtaUkz6NFSQaa3Tr7GKgjZkN7Fd0QG3KkQoFvW-c46QsQ3A87t6MwhoTVr7Udk-7U3ShvZKhGeIs9Q075NR56X9U616ZNwR1EhXuhWHyw7J9bm3oI1DYewIV1w7Fko-PPVPJtIn9fbxpibIwK9IwKyjzymCKQmuPfEOy-BaskaEpqXEuJuEQzkak4f-np5jXdCL8rSACoTr4HQFJlIsGQ")
                {
                    string sds = "";
                    string ddddd = "";
                }
                var tmp = Cache.GetString("mss3");
                var userid = Cache.GetString(rediskey);

            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

    }
}
