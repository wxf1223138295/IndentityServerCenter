using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServerOneNew.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServerOneNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class IdentityController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityParser<ApplicationUser> _appUserParser;
        // GET api/values


        public IdentityController(IHttpContextAccessor httpContextAccessor, IIdentityParser<ApplicationUser> appUserParser)
        {
            _httpContextAccessor = httpContextAccessor;
            _appUserParser = appUserParser;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var user=_appUserParser.Parse(_httpContextAccessor.HttpContext.User);

            var requestHost=_httpContextAccessor.HttpContext.Request.Host;

            var prop = await _httpContextAccessor.HttpContext.AuthenticateAsync();

            var identityToken= prop.Properties.GetTokenValue("id_token");

            var accseetokem=await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            ReturnModel model = new ReturnModel
            {
                Accseetoken = accseetokem,
                IdentityToken = identityToken,
                RequestHost = requestHost.ToString(),
                UserName = user.Name
            };


            return new JsonResult(model);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}