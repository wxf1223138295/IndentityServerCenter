using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MVCClient.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MVCClient.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            ViewData["OpenidUrl"] = _configuration["OPenidurl"];
            ViewData["HybridUrl"] = _configuration["HybridUrl"];

            return View();
        }
        public async Task<ActionResult<string>> PasswordModel()
        {
            var client = _clientFactory.CreateClient("getjwt");

            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Policy ={
                    RequireHttps = false
                }
            });

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

            return View();
        }

        public async Task<ActionResult<string>> ClientModel()
        {
            var client = _clientFactory.CreateClient("getjwt");


            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Policy ={
                RequireHttps = false
            }
            });

            var result = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
                Scope = "api1"
            });

            var client1 = _clientFactory.CreateClient("getapiserverone");
            client1.SetBearerToken(result.AccessToken);


            var t = await client1.GetAsync("");


            ViewData["IsSuccess"] = t.IsSuccessStatusCode;
            var resultResponse = await t.Content.ReadAsStringAsync();
            if (t.IsSuccessStatusCode)
            {
                ViewData["AccessToken"] = result.AccessToken;
                ViewData["RresultResponse"] = resultResponse;
            }
            else
            {
                ViewData["ErrorInfo"] = t.StatusCode;
            }

            return View();
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
