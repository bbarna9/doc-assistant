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
    public class DoctorAuthService : IAuthService<Doctor>
    {
        private readonly Dictionary<long, byte []> _accessTokens;
       // private readonly Dictionary<long, byte[]> _assistantAccessTokens;
            
        private readonly IRepository<Doctor> _doctorRepository;
        
        public DoctorAuthService(IRepository<Doctor> doctorRepository)
        {
            this._accessTokens = new Dictionary<long, byte[]>();
          //  this._assistantAccessTokens = new Dictionary<long, byte[]>();
            
            this._doctorRepository = doctorRepository;
        }
        
        public async Task<Doctor> Auth(string username, string password)
        {
            Doctor doctor = await this._doctorRepository.Where(
                doctor => doctor.Username.Equals(username));

            if (doctor == null) return null;

            if (!SecurityUtils.VerifyPassword(password, doctor.Password)) return null;

            if (_accessTokens.ContainsKey(doctor.Id)) _accessTokens.Remove(doctor.Id);

            return doctor;
        }

        public async Task<string> GenerateAccessToken(Doctor doctor)
        {
           // var roles = new Roles[] {Roles.Assistant, Roles.Doctor}.GetFormattedRoles().ToArray();

           var (bytes, token) = TokenFactory.CreateAccessToken(Roles.Doctor,doctor.Id); //SecurityUtils.GenerateAccessToken(doctor.Id,roles);

           _accessTokens.Add(doctor.Id,bytes);
            
            return token;
        }
        public async Task<string> GenerateAccessToken(Assistant assistant)
        {
            //var roles = new Roles[] {Roles.Assistant}.GetFormattedRoles().ToArray();
            
            var (bytes, token) = TokenFactory.CreateAccessToken(Roles.Assistant,assistant.Id); //SecurityUtils.GenerateAccessToken(assistant.Id,roles);

            _accessTokens.Add(assistant.Id,bytes);
            
            return token;
        }

        public async Task<(bool, long)> Authorize(/*HttpContext context, */string accessToken)
        {
            throw new NotImplementedException();
           // return SecurityUtils.VerifyAccessToken(_doctorAccessTokens, accessToken);
        }
        
        public async Task<bool> Logout(string accessToken)
        {
            throw new NotImplementedException();
            /*var result = SecurityUtils.VerifyAccessToken(_accessTokens, accessToken);
            if(!result.Item1) return false;

            this._accessTokens.Remove(result.Item2);

            return true;*/
        }

        public (bool, long, IEnumerable<Roles> ) VerifyAccessToken(string token)
        {

            var splitted = token.Split('.');

            if (splitted.Length != 2) return (false,-1,null);

            var jsonString = Encoding.Default.GetString(Convert.FromHexString(splitted[0]));

            var jsonObject = JObject.Parse(jsonString);

            var id = (long) jsonObject["userId"];

            if (!_accessTokens.ContainsKey(id)) return (false,-1,null);

            byte[] salt = _accessTokens[id];
            
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            var constructedToken = SecurityUtils.HashStringHex(Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonObject,settings)), salt);
            if (constructedToken != splitted[1]) return (false, -1, null);

            var rolesString = jsonObject["roles"].ToObject<string []>();

            if (rolesString == null) return (false,-1,null);
            
            var roleList = new List<Roles>();
            foreach (var roleString in rolesString) roleList.Add(IAuthService<Doctor>.GetRoleByName(roleString));

            return (true, id, roleList);

        }
    }
}