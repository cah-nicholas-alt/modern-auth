using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace accounts.api.pursuit
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

            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", (options) =>
                {
                    options.Authority = "http://id.pursuit.local:5000";
                    options.Audience = "http://accounts.api.pursuit.local:5001/";
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin",
                    policy => policy.RequireClaim("scope", "PursuitAccountsApi.Admin"));
                options.AddPolicy("read",
                    policy => policy.RequireClaim("scope", "PursuitAccountsApi.ReadAccounts"));
            });

            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors(options =>
                options
                    .SetIsOriginAllowed(o =>
                        new Uri(o).Host.EndsWith("pursuit.local")
                        || new Uri(o).Host == "localhost")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
