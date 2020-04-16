using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication14
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[] {
             //secretapi:标识名称，Secret Api：显示名称，可以自定义
             new ApiResource("secretapi","Secret Api",new List<string>(){ ClaimTypes.Role,ClaimTypes.Name,"Project"})
         };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new Client[] {

                new Client()
                {
                    //客户端Id
                     ClientId="apiClientCd",
                     //客户端密码
                     ClientSecrets={new Secret("apiSecret".Sha256()) },
                     //客户端授权类型，ClientCredentials:客户端凭证方式
                     AllowedGrantTypes=GrantTypes.ClientCredentials,
                     //允许访问的资源
                     AllowedScopes={
                        "secretapi"
                    }
                },
                new Client()
                {
                    //客户端Id
                     ClientId="apiClientPassword",
                     //客户端密码
                     ClientSecrets={new Secret("apiSecret".Sha256()) },
                     //客户端授权类型，ClientCredentials:客户端凭证方式
                     AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                     //允许访问的资源
                     AllowedScopes={
                        "secretapi"
                    }
                },
                new Client()
                {
                    //客户端Id
                     ClientId="apiClientImpl",
                     ClientName="ApiClient for Implicit",
                     //客户端授权类型，Implicit:隐藏模式
                     AllowedGrantTypes=GrantTypes.Implicit,
                     //允许登录后重定向的地址列表，可以有多个
                    RedirectUris = {"https://localhost:5002/auth.html" },
                     //允许访问的资源
                     AllowedScopes={
                        "secretapi"
                    },
                     //允许将token通过浏览器传递
                     AllowAccessTokensViaBrowser=true
                },
                new Client()
               {
                   //客户端Id
                    ClientId="apiClientCode",
                    ClientName="ApiClient for Code",
                    //客户端密码
                    ClientSecrets={new Secret("apiSecret".Sha256()) },
                    //客户端授权类型，Code:授权码模式
                    AllowedGrantTypes=GrantTypes.Code,
                    //允许登录后重定向的地址列表，可以有多个
                   RedirectUris = {"https://localhost:5002/auth.html"},
                    //允许访问的资源
                    AllowedScopes={
                       "secretapi"
                   }
               }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
        {
            new TestUser()
            {
                //用户名
                 Username="apiUser",
                 //密码
                 Password="apiUserPassword",
                 //用户Id
                 SubjectId="0",
                 //密码模式可以自己添加claim
                  Claims=new List<Claim>(){
                     new Claim(ClaimTypes.Role,"admin"),
                     new Claim("Project","正式项目")
                 }
            },
                 new TestUser()
                {
                    //用户名
                     Username="apiUserGuest",
                     //密码
                     Password="apiUserPassword",
                     //用户Id
                     SubjectId="1",
                     Claims=new List<Claim>(){
                         new Claim(ClaimTypes.Role,"guest"),
                         new Claim("Project","测试项目")
                     }
                }
        };
        }

        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            { };

        public static IEnumerable<Client> Clients =>
            new Client[]
            { };

    }
}
