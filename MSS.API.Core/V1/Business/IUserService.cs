using MSS.API.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSS.API.Core.V1.Business
{
    public interface IUserService
    {
        Task<UserResponse> FindUser(int id);
    }
}
