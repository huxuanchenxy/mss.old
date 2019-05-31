using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Security.Claims;

namespace MSS.API.Ids
{
    public class Config
    {

        public static IEnumerable<ApiResource> GetApiResources() //api
        {
            return new List<ApiResource>(){
                new ApiResource("MSS_WEBAPI","my api")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {

                new Client(){
                    ClientId="client",
                    //客户端模式
                     AllowedGrantTypes=GrantTypes.ClientCredentials,
                     ClientSecrets={new Secret("secret".Sha256())},
                     AllowedScopes={ "MSS_WEBAPI" }
                },
                new Client(){
                    ClientId="pwdClient",
                    //OAuth密码模式
                     AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                     ClientSecrets={new Secret("secret".Sha256())},
                     RefreshTokenUsage = TokenUsage.ReUse,
                     RefreshTokenExpiration = TokenExpiration.Absolute,
                     AlwaysIncludeUserClaimsInIdToken = true,
                     AccessTokenLifetime = 3600,
                     AllowOfflineAccess = true,
                     AllowedScopes={ "MSS_WEBAPI" }
                },


            };
        }

        public static List<TestUser> GetTestUser()
        {
            return new List<TestUser>() {
                new TestUser(){
                    SubjectId = "1",
                    Username ="zps",
                    Password = "zps",
                    Claims = new List<Claim>(){
                        new Claim("role","zps"),
                        new Claim("aaa","asdasdsd"),
                        new Claim(JwtClaimTypes.Role,"superadmin"),
                    }
                },
                 new TestUser(){
                    SubjectId = "2",
                    Username ="admin",
                    Password = "admin",
                     Claims = new List<Claim>(){
                        new Claim("role","admin")
                    }
                }
            };
        }

    }
}
