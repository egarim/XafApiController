using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Security;
using DevExpress.Blazor.Reporting;
using DevExpress.ExpressApp.ReportsV2.Blazor;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XafApiController.Blazor.Server.Services;
using XafApiController.Blazor.Server.Controllers;
using XafApiController.Module.BusinessObjects;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace XafApiController.Blazor.Server
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
            services.AddHttpContextAccessor();
            services.AddSingleton<XpoDataStoreProviderAccessor>();
            services.AddScoped<CircuitHandler, CircuitHandlerProxy>();
            services.AddXaf<XafApiControllerBlazorApplication>(Configuration);
            services.AddXafReporting();
            services.AddXafSecurity(options =>
            {
                options.RoleType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole);
                options.UserType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser);
                options.Events.OnSecurityStrategyCreated = securityStrategy => ((SecurityStrategy)securityStrategy).RegisterXPOAdapterProviders();
                options.SupportNavigationPermissionsForTypes = false;
            }).AddExternalAuthentication<HttpContextPrincipalProvider>()
            .AddAuthenticationStandard(options =>
            {
                options.IsSupportChangePassword = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/LoginPage";
            });


            List<Type> types = new List<Type>();
            types.Add(typeof(DomainObject2));

            services.AddSingleton(typeof(IObjectSpaceService), new ObjectSpaceService(types, Configuration.GetConnectionString("ConnectionString")));




            JwtService JwtService = new JwtService(JwtService.GenerateKey(128), "Joche");

            var GeneratedOn = DateTime.Now;

            var claims = new List<Claim>
            {
                new Claim("iss", "Joche", ClaimValueTypes.String),
                new Claim("sub", "", ClaimValueTypes.String),
                new Claim("aud", "", ClaimValueTypes.String),
                new Claim("exp", EpochTime.GetIntDate(GeneratedOn).ToString(), ClaimValueTypes.Integer64),
                new Claim("iat", EpochTime.GetIntDate(GeneratedOn.AddDays(1)).ToString(), ClaimValueTypes.Integer64),
            };

            // Serialize To Json
            JwtPayload payload = new JwtPayload(claims);

            //HACK to validate token https://jwt.io/

            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("Token:"+JwtService.JwtPayloadToToken(payload));
            Debug.WriteLine("");
            Debug.WriteLine("");

            //HACK test token:eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJKb2NoZSIsInN1YiI6IiIsImF1ZCI6IiIsImV4cCI6MTYyNTY1Nzk2OCwiaWF0IjoxNjI1NzQ0MzY4fQ.LQvvrjXYGwvRxnBJwVtfGInjn_P4wr3Ceg425Sc4mSk

            services.AddSingleton(typeof(IJwtService), JwtService);
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            app.UseXaf();
            app.UseDevExpressBlazorReporting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllers();
            });
        }
    }
}
