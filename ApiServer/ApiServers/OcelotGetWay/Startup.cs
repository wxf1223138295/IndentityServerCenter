using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotGetWay.Configuration;

namespace OcelotGetWay
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = _env.GetAppConfiguration();
        }
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IHostingEnvironment _env;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationProviderKey = "OcelotKey";

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie("Cookies", o => o.ExpireTimeSpan = TimeSpan.FromMinutes(1))
                .AddOpenIdConnect(options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Authority = _appConfiguration["IdentityServerCenterUrl"];
                    options.RequireHttpsMetadata = false;
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ResponseType = "code id_token";
                    //options.SignedOutRedirectUri = callBackUrl.ToString();
                    options.ClientId = "mvcHybrid";
                    options.ClientSecret = "secret";

                    options.Scope.Add("api1");
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("offline_access");
                    options.GetClaimsFromUserInfoEndpoint = true;
                });

            services.AddOcelot();

            services.AddLogging(p => { p.AddConsole(); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseOcelot().Wait();
        }
    }
}
