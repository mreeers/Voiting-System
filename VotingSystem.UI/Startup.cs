using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VoitingSystem;

namespace VotingSystem.UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IVotingPollFactory, VotingPollFactory>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapDefaultControllerRoute();

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }

    public class Service201
    {
        public int GetCode() => 201;
    }

    public class CustomMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly Service201 _service;

        public CustomMiddleware(RequestDelegate request, Service201 service)
        {
            _request = request;
            _service = service;
        }

        public async Task Invoke(HttpContext context)
        {
            // request is comming in

            context.Response.StatusCode = _service.GetCode();
            context.Response.ContentType = "application/json";

            await _request(context);

            //request is going out
        }
    }
}
