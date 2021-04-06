using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Filters;
using DocAssistantWebApi.Services.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
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
            services.AddSingleton<IAuthService, AuthService>();
            
           /* services.AddAuthorization(options =>
                options.AddPolicy("DoctorAuth", policy => policy.Requirements.Add(new DoctorAuthRequirement()))
            );*/
            
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

           

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}