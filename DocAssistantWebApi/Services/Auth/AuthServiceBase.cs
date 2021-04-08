using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Filters;
using DocAssistantWebApi.Utils;
using DocAssistantWebApi.Utils.Factories;
using Newtonsoft.Json;

namespace DocAssistantWebApi.Services.Auth
{
    public abstract class AuthServiceBase<T> : IAuthService<T> where T : Staff
    {

        private readonly Dictionary<long, byte []> _accessTokens;
        protected readonly IRepository<T> Repository;

        protected AuthServiceBase(IRepository<T> repository)
        {
            this._accessTokens = new Dictionary<long, byte[]>();
            this.Repository = repository;
        }

        public abstract Task<T> Auth(string username, string password);
        protected async Task<T> Auth(T entity,string plainPassword)
        {
            if (entity == null) return null;

            if (!SecurityUtils.VerifyPassword(plainPassword, entity.Password)) return null;

            if (_accessTokens.ContainsKey(entity.Id)) _accessTokens.Remove(entity.Id);

            return await Task.FromResult(entity);
        }

        public virtual async Task<string> GenerateAccessToken(T entity)
        {
            var entityType = entity.GetType();

            (byte[], string)? tokenData = null;
            
            if (entityType.IsAssignableTo(typeof(Doctor)))
                tokenData = TokenFactory.CreateAccessToken(Roles.Doctor,entity.Id);
            else if (entityType.IsAssignableTo(typeof(Assistant)))
                tokenData = TokenFactory.CreateAccessToken(Roles.Assistant, entity.Id);
            else throw new InvalidEnumArgumentException();

            _accessTokens.Add(entity.Id,tokenData.Value.Item1);
            
            return await Task.FromResult(tokenData.Value.Item2);
        }
        
        public async Task<bool> Logout(long id)
        {
            if (!_accessTokens.ContainsKey(id)) return await Task.FromResult(false);

            _accessTokens.Remove(id);
            
            return await Task.FromResult(true);
        }

        private bool CompareTokens(string serializedToken,byte [] salt, string fromToken)
        {
            var constructedToken = SecurityUtils.HashStringHex(Encoding.Default.GetBytes(serializedToken), salt);
            return constructedToken == fromToken;
        }

        private bool HasTokenExpired(DateTime expiration)
        {
            return expiration > DateTime.Now;
        }
        
        public (bool, long, IEnumerable<Roles> ) VerifyAccessToken(string token)
        {

            var splitted = token.Split('.');

            if (splitted.Length != 2) return (false,-1,null);

            var jsonString = Encoding.Default.GetString(Convert.FromHexString(splitted[0]));

            var jsonObject = JsonConvert.DeserializeObject<TokenFactory.AccessTokenPayload>(jsonString);

            if (!_accessTokens.ContainsKey(jsonObject.UserId)) return (false,-1,null);
            if(!HasTokenExpired(jsonObject.Expires)) return (false,-1,null);

            byte[] salt = _accessTokens[jsonObject.UserId];

            string serializedToken = JsonConvert.SerializeObject(jsonObject);

            if (!CompareTokens(serializedToken, salt, splitted[1])) return (false, -1, null);

            if (jsonObject.Roles == null) return (false,-1,null);
            
            var roleList = new List<Roles>();
            foreach (var roleString in jsonObject.Roles) roleList.Add(IAuthService<Doctor>.GetRoleByName(roleString));

            return (true, jsonObject.UserId, roleList);

        }
    }
}