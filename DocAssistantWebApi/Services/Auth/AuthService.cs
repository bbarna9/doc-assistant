using System;
using System.Threading.Tasks;
using DocAssistantWebApi.Database.DbModels;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Utils;
using System.Collections.Generic;
using System.Security.Policy;

namespace DocAssistantWebApi.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly Dictionary<long, string> _accessTokens;
            
        private readonly IRepository<Doctor> _doctorRepository;
        
        public AuthService(IRepository<Doctor> doctorRepository)
        {
            this._accessTokens = new Dictionary<long, string>();
            this._doctorRepository = doctorRepository;
        }
        
        public async Task<Doctor> Auth(string username, string password)
        {
            Doctor doctor = await this._doctorRepository.Where(
                doctor => doctor.Username.Equals(username));

            if (doctor == null) return null;

            if (!SecurityUtils.VerifyPassword(password, doctor.Password)) return null;

            if (_accessTokens.ContainsKey(doctor.Id)) _accessTokens.Remove(doctor.Id); // log out

            return doctor;
        }

        public async Task<string> GenerateAccessToken(Doctor doctor)
        {
            string token = SecurityUtils.GenerateAccessToken(doctor.Id);

            _accessTokens.Add(doctor.Id,token.Split('.')[1]);
            
            return token;
        }

        public async Task<bool> Authorize(string accessToken)
        {
            return SecurityUtils.VerifyAccessToken(_accessTokens, accessToken,out _);
        }
        
        public async Task<bool> Logout(string accessToken)
        {
            long docId;
            
            if(!SecurityUtils.VerifyAccessToken(_accessTokens, accessToken,out docId)) return false;

            this._accessTokens.Remove(docId);

            return true;
        }
    }
}