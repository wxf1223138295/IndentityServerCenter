using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCClientHybrid.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace MVCClientHybrid.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;

        public HomeController(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IIdentityParser<ApplicationUser> appUserParser)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _appUserParser = appUserParser;
        }

        [Authorize]
        public async Task<ActionResult<ViewModelSimple>> HybridModel()
        {
            var prop = await _httpContextAccessor.HttpContext.AuthenticateAsync();

           
            var userInfoClient = new UserInfoClient(_configuration["Userinfourl"]);

            

            ViewModelSimple simple = new ViewModelSimple();
           
            simple.AccessToken = prop.Properties.GetTokenValue("access_token");

            var userInfo =await userInfoClient.GetAsync(simple.AccessToken);
           

        
            ClaimsIdentity claimsIdentity=new ClaimsIdentity(userInfo.Claims);
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            var use = _appUserParser.Parse(principal);

            simple.UserName = use.Name;
            simple.TelNum = use.PhoneNumber;
            simple.IdToken = prop.Properties.GetTokenValue("id_token");
            simple.Refreshtoken = prop.Properties.GetTokenValue("refresh_token");
            //var rer= prop.Properties.GetTokenValue("access_token");
            // var tt=prop.Properties.GetTokens();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var reToken = await HttpContext.GetTokenAsync("refresh_token");

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
