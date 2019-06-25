using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSS.API.Org
{
    public class RedisMQ:IRedisMQ
    {
        //读取连接Redis字符串
        private readonly string connectRedis = "10.89.36.204:6379,password=Test01supersecret,defaultDatabase=7,poolsize=50,ssl=false,writeBuffer=10240";  
        //定义一个Redis客户端对象
        static CSRedisClient _RedisMQ;
        //连接Redis
        public CSRedisClient ConnectCSRedis()
        {
            //如果已经连接实例，直接返回
            if (_RedisMQ != null)
            {
                return _RedisMQ;
            }
            return _RedisMQ = new CSRedisClient(connectRedis);
        }
        //释放Redis
        public void DisposeCSRedis()
        {
            _RedisMQ.Dispose();
        }

    }
}
