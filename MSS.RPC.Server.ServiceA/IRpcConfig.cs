using System;
using System.Collections.Generic;
using System.Text;

namespace MSS.RPC.Server.ServiceA
{
    public interface IRpcConfig
    {
        void Start(int port);
    }
}
