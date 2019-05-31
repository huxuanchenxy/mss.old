using MSS.API.Dao.Interface;
using MSS.API.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSS.API.Model.DTO;

namespace MSS.API.Core.V1.Business
{
    public class UserService: IUserService
    {
        //private readonly ILogger<UserService> _logger;
        private readonly IUserRepo<User> _userRepo;

        public UserService(IUserRepo<User> userRepo)
        {
            //_logger = logger;
            _userRepo = userRepo;
        }

        public async Task<UserResponse> FindUser(int id)
        {
            UserResponse ret = new UserResponse();
            var data =  await _userRepo.FindUser(id);
            //用automapper 做mapper
            return ret;
        }
    }
}
