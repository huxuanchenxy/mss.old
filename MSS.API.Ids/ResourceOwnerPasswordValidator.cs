using MSS.API.Dao;
using MSS.API.Model.Data;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MSS.API.Dao.Interface;

namespace MSS.API.Ids
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
         private readonly IUserRepo<User> _userRepo;

        public ResourceOwnerPasswordValidator(IUserRepo<User> userRepo)
        {
            _userRepo = userRepo; //DI
        }
       
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
                var user = await _userRepo.IsValid(context.UserName, context.Password);
                //user = null;
                if (user != null)
                {
                    context.Result = new GrantValidationResult(
                     subject: context.UserName,
                     authenticationMethod: "custom",
                     claims: GetUserClaims(user));
                    return;
                }
                else
                {

                    //验证失败
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
                }
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }
        //可以根据需要设置相应的Claim
        private Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
            new Claim("UserId", user.ID.ToString()),
             //new Claim(JwtClaimTypes.Name,user.ClientId)
           
            };
        }
    }
}