using MSS.API.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSS.API.Dao.Interface
{
    public interface IUserRepo<T> where T:BaseEntity
    {
        Task<User> FindUser(int Id);
        Task<User> IsValid(string clientId, string Password);
    }
}
