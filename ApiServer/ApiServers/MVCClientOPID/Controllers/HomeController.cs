using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCClientOPID.Models;

namespace MVCClientOPID.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;

        public HomeController(IHttpContextAccessor httpContextAccessor, IIdentityParser<ApplicationUser> appUserParser, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _appUserParser = appUserParser;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [Authorize]
        public async Task<ActionResult<ViewModelSimple>> SimpleModel()
        {


            var userInfoClient = new UserInfoClient(_configuration["Userinfourl"]);


            

            var prop=await _httpContextAccessor.HttpContext.AuthenticateAsync();

            ViewModelSimple simple=new ViewModelSimple();
            simple.AccessToken = prop.Properties.GetTokenValue("access_token");
            var userInfo = await userInfoClient.GetAsync(simple.AccessToken);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims);
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            var use = _appUserParser.Parse(principal);

          
            simple.UserName = use.Name;
            simple.TelNum = use.PhoneNumber;
            simple.IdToken= prop.Properties.GetTokenValue("id_token");
            
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client1 = _clientFactory.CreateClient("getapiserverone");

            client1.SetBearerToken(simple.AccessToken);

            var responseMessage = await client1.GetAsync("");


            var resultResponse = await responseMessage.Content.ReadAsStringAsync();
            simple.ResponseMessage = resultResponse;

            return View(simple);
        }
        public IActionResult Index()
        {
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
