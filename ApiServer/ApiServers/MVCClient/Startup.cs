using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            });


            services.AddControllersWithViews()
                .AddNewtonsoftJson();
            services.AddRazorPages();

            services.AddHttpClientServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
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
                    c => { c.BaseAddress = new Uri("http://localhost:5000"); })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5)); //Sample. Default lifetime is 2 minutes 
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
                    c => { c.BaseAddress = new Uri("http://localhost:5003/api/Identity"); })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            return services;
        }
    }
}
