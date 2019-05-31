using System;
using System.Collections.Generic;
using System.Text;

namespace MSS.API.Model.Data
{
    public class User:BaseEntity
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
