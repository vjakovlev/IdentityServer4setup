using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config => 
            {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";

            })
            .AddCookie("Cookie")
            .AddOpenIdConnect("oidc", config => 
            {
                config.Authority = "https://localhost:44321/";
                config.ClientId = "client_id_mvc";
                config.ClientSecret = "viktor_mvc";
                config.SaveTokens = true;
                config.ResponseType = "code";

                //deleting claims
                config.ClaimActions.DeleteClaim("amr");
                config.ClaimActions.DeleteClaim("s_hash");

                //configure cookie claim mapping
                config.ClaimActions.MapUniqueJsonKey("RAW.viktor","cl.viktor");

                // 2 trips to load claims in to the cookie - id token is smaller
                config.GetClaimsFromUserInfoEndpoint = true;

                //configure scope
                config.Scope.Clear();
                config.Scope.Add("openid");
                config.Scope.Add("viktor.scope");
                config.Scope.Add("ApiOne");
                config.Scope.Add("ApiTwo");
                config.Scope.Add("offline_access");
            });

            services.AddHttpClient();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
