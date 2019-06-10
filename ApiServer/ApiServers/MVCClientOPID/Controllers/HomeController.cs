using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCClientOPID.Models;

namespace MVCClientOPID.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;

        public HomeController(IHttpContextAccessor httpContextAccessor, IIdentityParser<ApplicationUser> appUserParser)
        {
            _httpContextAccessor = httpContextAccessor;
            _appUserParser = appUserParser;
        }

        [Authorize]
        public async Task<ActionResult<string>> Index2()
        {
            var use=_appUserParser.Parse(_httpContextAccessor.HttpContext.User);
            return $"简易模式---access_token:";
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
