using System;
using System.Threading;
using System.Threading.Tasks;
using static UserService;

namespace MSS.RPC.Server.ServiceC.Impl
{
    public class UserServiceImpl : Iface
    {
        public User Get(int id)
        {
            return new User
            {
                Id =12,
                Name = "成天",
                Remark = "热爱编程，热爱分享，热爱..."
            };
        }
    }
}
