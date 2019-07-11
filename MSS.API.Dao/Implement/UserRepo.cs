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

        public async Task<User> IsValid(string clientId, string Password)
        {

            return await WithConnection(async c => {


                //var user = await c.QueryFirstOrDefaultAsync<User>("select * from ApiUser where ClientId=@ClientId", new { clientId = clientId });
                var user = await c.QueryFirstOrDefaultAsync<User>(" SELECT * FROM User WHERE id = @Id ", new { Id = 1 });
                if (user == null)
                    return null;

                //var pwd_decypt = Rijndael.Decrypt(user.Password, user.Salt, KeySize.Aes256);
                //var isvalid = pwd_decypt == Password;
                //if (isvalid)
                    return user;
                //return null;

            });
        }
    }
}
