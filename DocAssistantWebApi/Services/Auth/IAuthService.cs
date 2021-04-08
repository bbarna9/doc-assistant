using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Filters;

namespace DocAssistantWebApi.Services.Auth
{
    public interface IAuthService<T>
    {
        public Task<T> Auth(string username, string password);
        public Task<string> GenerateAccessToken(T entity);
        public (bool, long, IEnumerable<Roles>) VerifyAccessToken(string token);
      //  public Task<string> GenerateAccessToken(Assistant assistant););
        public Task<bool> Logout(long id);

        public static Roles GetRoleByName(string name)
        {
            Enum.TryParse(name, true, out Roles result);
            
            return result;
        }
    }
}