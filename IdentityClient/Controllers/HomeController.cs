using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityClient.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using IdentityModel.Client;

namespace IdentityClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> GetData(string type, string userName, string password,string code)
        {
            type = type ?? "client";
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
                return new JsonResult(new { err = disco.Error });
            TokenResponse token = null;
            switch (type)
            {
                case "client":
                    token = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
                    {
                        //获取Token的地址
                        Address = disco.TokenEndpoint,
                        //客户端Id
                        ClientId = "apiClientCd",
                        //客户端密码
                        ClientSecret = "apiSecret",
                        //要访问的api资源
                        Scope = "secretapi"
                    });
                    break;
                case "password":
                    token = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
                    {
                        //获取Token的地址
                        Address = disco.TokenEndpoint,
                        //客户端Id
                        ClientId = "apiClientPassword",
                        //客户端密码
                        ClientSecret = "apiSecret",
                        //要访问的api资源
                        Scope = "secretapi",
                        UserName = userName,
                        Password = password
                    });
                    break;
                case "code":
                    token = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest()
                    {
                        Address = disco.TokenEndpoint,
                        ClientId = "apiClientCode",
                        //客户端密码
                        ClientSecret = "apiSecret",
                        Code = code,
                        RedirectUri = "https://localhost:5002/auth.html"
                    });
                    break;
            }
            if (token.IsError)
                return new JsonResult(new { err = token.Error });
            client.SetBearerToken(token.AccessToken);
            string data = await client.GetStringAsync("https://localhost:5001/api/identity");
            JArray json = JArray.Parse(data);
            return new JsonResult(json);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
