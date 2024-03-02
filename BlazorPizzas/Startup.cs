using BlazorPizzas.Data;
using BlazorPizzas.Opts;
using BlazorPizzas.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorPizzas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            //services.AddSingleton<WeatherForecastService>();
            //services.AddSingleton<IPizzaManager, InMemoryPizzaManager>();

            services.AddOptions();
            services.Configure<ApiOptions>(Configuration.GetSection(key: "Api"));

            services.AddHttpClient<IPizzaManager, HttpPizzaManager>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<ApiOptions>>();

                client.BaseAddress = new Uri(options.Value.Url);
            })
                .AddPolicyHandler(GetPolicy());

        }

        private IAsyncPolicy<HttpResponseMessage> GetPolicy()
            => HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(retryCount: 3, i=> TimeSpan.FromSeconds(300 + (i * 100)));


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
