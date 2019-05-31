using System;
using System.Collections.Generic;
using System.Text;
using MSS.API.Dao.Interface;
using MSS.API.Dao;
using MSS.API.Model.Data;
using System.Threading.Tasks;
using Dapper;

namespace MSS.API.Dao.Implement
{
    public class UserRepo : BaseRepo, IUserRepo<User>
    {
        public UserRepo(DapperOptions options) : base(options) { }
        public async Task<User> FindUser(int Id)
        {
            return await WithConnection(async c =>
            {
                var result = await c.QueryFirstOrDefaultAsync<User>(" SELECT * FROM User WHERE id = @Id ", new { Id = Id });
                return result;
            });
        }
    }
}
