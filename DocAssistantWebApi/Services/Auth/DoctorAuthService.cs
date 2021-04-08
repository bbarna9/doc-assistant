using System;
using System.Threading.Tasks;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Filters;
using DocAssistantWebApi.Utils.Factories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DocAssistantWebApi.Services.Auth
{
    public class DoctorAuthService : AuthServiceBase<Doctor>
    {
        public DoctorAuthService(IRepository<Doctor> doctorRepository) : base(doctorRepository)
        {
        }

        public override async Task<Doctor> Auth(string username, string password)
        {
            Doctor doctor = await this.Repository.Where(
                doctor => doctor.Username.Equals(username));

            return await base.Auth(doctor, password);
        }

        public override async Task<string> GenerateAccessToken(Doctor doctor)
        {
            return await base.GenerateAccessToken(doctor);
        }
    }
}