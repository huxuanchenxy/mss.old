using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSS.API.Org
{
    public interface IRedisMQ
    {
        //连接Redis
        CSRedisClient ConnectCSRedis();
        void DisposeCSRedis();
    }
}
