using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace MVCClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication();

            services.AddHttpClientServices(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
    public static class ServiceCollectionExtensions
    {
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }
        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
        /// <summary>
        /// HttpClient
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("getjwt",
                    c => { c.BaseAddress = new Uri(configuration["IdentityServerCenterUrl"]); })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                      .ConfigurePrimaryHttpMessageHandler(() =>
                      {
                          return new HttpClientHandler()
                          {
                              ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true
                          };
                      }
                ); //Sample. Default lifetime is 2 minutes 
                                                              //.AddPolicyHandler(GetRetryPolicy())
                                                              //.AddPolicyHandler(GetCircuitBreakerPolicy());

            //services.AddHttpClient("getreports", c =>
            //{
            //    c.BaseAddress = new Uri(configuration["Urls:GetReportListUrl"]);
            //}).SetHandlerLifetime(TimeSpan.FromMinutes(7))  //Sample. Default lifetime is 2 minutes
            //       .AddPolicyHandler(GetRetryPolicy())
            //       .AddPolicyHandler(GetCircuitBreakerPolicy());

            //services.AddHttpClient("downLoadReport", c =>
            //{
            //    c.BaseAddress = new Uri(configuration["Urls:DownLoadReportUrl"]);
            //    c.DefaultRequestHeaders.Add("token", "");
            //    //c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //}).SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
            //                                                //.AddHttpMessageHandler<HttpReportDelegatingHandler>()
            //      .ConfigurePrimaryHttpMessageHandler(() =>
            //      {
            //          return new HttpClientHandler()
            //          {
            //              ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true
            //          };
            //      });
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());


            services.AddHttpClient("getapiserverone",
                    c => { c.BaseAddress = new Uri(configuration["apiserviceoneurl"]); })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            return services;
        }
    }
}
