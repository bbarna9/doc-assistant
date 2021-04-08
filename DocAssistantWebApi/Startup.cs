using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Filters;
using DocAssistantWebApi.Services.Auth;
using DocAssistantWebApi.Services.Auth.AuthHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Patient = DocAssistant_Common.Models.Patient;

namespace DocAssistantWebApi
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DocAssistantWebApi", Version = "v1"});
            });

            services.AddSingleton<IRepository<Doctor>, DoctorRepository>();
            services.AddSingleton<IRepository<Patient>, PatientRepository>();
            services.AddSingleton<IRepository<Assistant>, AssistantRepository>();
            
            services.AddSingleton<IAuthService<Doctor>, DoctorAuthService>();
            services.AddSingleton<IAuthService<Assistant>, AssistantAuthService>();

            services.AddAuthentication("AuthToken")
                .AddScheme<AuthTokenSchemeOptions, AuthTokenHandler>("AuthToken",null);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AssistantRequirement", policy => policy.Requirements.Add(new AuthRoleRequirement(
                    new Roles[]
                    {
                        Roles.Assistant,
                        Roles.Doctor
                    }))
                );
                options.AddPolicy("DoctorRequirement", policy => policy.Requirements.Add(new AuthRoleRequirement(
                    new Roles[]
                    {
                        Roles.Doctor
                    }))
                );
            });

           /* services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
            });*/
            
           // services.AddAuthorization(options =>
                //options.AddPolicy("RequireDoctorRole", policy => policy.Requirements.Add(new DoctorAuthRequirement()))
           // );
            
            SQLiteDatabaseContext.ConnectionString = Configuration.GetConnectionString("SQLiteDatabase");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocAssistantWebApi v1"));
            }

            app.UseExceptionHandler(exceptionHandler => exceptionHandler.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;

                if (!context.Response.HasStarted)
                {
                    if (exception is GenericRequestException ex)
                    {
                        var response = new
                        {
                            title = ex.Title,
                            error = ex.Error,
                            status = ex.StatusCode
                        };
                    
                        context.Response.StatusCode = ex.StatusCode;
                    
                        await context.Response.WriteAsJsonAsync(response);
                    }
                    else
                    {
                        var response = new { error = exception.Message, status = 500 };
                        await context.Response.WriteAsJsonAsync(response);
                    }
                }
            }));

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            
            
           /* app.Use(async (context, next) =>
            {
                Console.WriteLine("->>"+context.Request.Path);
                
               

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role,nameof(Roles.Assistant))
                };
                context.User.AddIdentity(new ClaimsIdentity(claims));
                Console.WriteLine(context.User.IsInRole(nameof(Roles.Assistant)));
                
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                
                Console.WriteLine(JsonConvert.SerializeObject(new
                {
                    Roles = new Roles [] {Roles.Assistant}
                },serializerSettings));
                
                //
                context.Items.Add("Id",1);
                //
                
                //context.GetRouteData().Routers.ToList().ForEach(elem => Console.WriteLine(elem.GetType()));
               
            /*    var cultureQuery = context.Request.Query["culture"];
                if (!string.IsNullOrWhiteSpace(cultureQuery))
                {
                    var culture = new CultureInfo(cultureQuery);

                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }

                
                await next();
            });
            */

           app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}