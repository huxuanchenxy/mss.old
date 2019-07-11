using System;
using System.Collections.Generic;
using System.Text;

namespace MSS.API.Model.Data
{
    public class user_operation_log : BaseEntity
    {
        public string controller_name { get; set; }
        public string action_name { get; set; }
        public string method_name { get; set; }
        public string acc_name { get; set; }
        public string user_name { get; set; }
        public string ip { get; set; }
        public string mac_add { get; set; }

    }
}
