using System.Collections.Generic;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Filters;

namespace DocAssistantWebApi.Services.Auth
{
    public class AssistantAuthService : IAuthService<Assistant>
    {
        
        private readonly Dictionary<long, byte []> _accessTokens;

        private readonly IRepository<Assistant> _assistantRepository;
        
        public AssistantAuthService(IRepository<Assistant> assistantRepository)
        {
            this._accessTokens = new Dictionary<long, byte[]>();

            this._assistantRepository = assistantRepository;
        }
        
        public Task<Assistant> Auth(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GenerateAccessToken(Assistant doctor)
        {
            throw new System.NotImplementedException();
        }

        public (bool, long, IEnumerable<Roles>) VerifyAccessToken(string token)
        {
            throw new System.NotImplementedException();
        }

        public Task<(bool, long)> Authorize(string accessToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Logout(string accessToken)
        {
            throw new System.NotImplementedException();
        }
    }
}