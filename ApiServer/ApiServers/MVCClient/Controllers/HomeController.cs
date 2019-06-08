using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MVCClient.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace MVCClient.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ActionResult<string>> Index2()
        {
            var client = _clientFactory.CreateClient("getjwt");

            var disco = await client.GetDiscoveryDocumentAsync("");

            var result = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",
                UserName = "1223138295@qq.com",
                Password = "Pass@word123",
                Scope = "api1"
            });

            var client1 = _clientFactory.CreateClient("getapiserverone");
            client1.SetBearerToken(result.AccessToken);

            var t = await client1.GetAsync("");

            if (!t.IsSuccessStatusCode)
            {
                var tr = t.StatusCode;
            }

            return "客户端模式-账号密码";
        }

        public async Task<ActionResult<string>> Index()
        {
            var client = _clientFactory.CreateClient("getjwt");

            var disco = await client.GetDiscoveryDocumentAsync("");


            var result = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
                Scope = "api1"
            });

            var client1 = _clientFactory.CreateClient("getapiserverone");
            client1.SetBearerToken(result.AccessToken);

            var t=await client1.GetAsync("");

            if (!t.IsSuccessStatusCode)
            {
                var tr=t.StatusCode;
            }

            return "客户端模式-id";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
