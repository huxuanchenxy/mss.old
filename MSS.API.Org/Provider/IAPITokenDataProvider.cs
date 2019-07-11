
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSS.Web.Org.Provider
{
    public interface IAPITokenDataProvider
    {
        Task<string> GetRedisAsync();
    }
}
