using System.Collections.Generic;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Filters;

namespace DocAssistantWebApi.Services.Auth
{
    public class AssistantAuthService : AuthServiceBase<Assistant>
    {
        public AssistantAuthService(IRepository<Assistant> assistantRepository) : base(assistantRepository) {}

        public override async Task<Assistant> Auth(string username, string password)
        {
            Assistant assistant = await this.Repository.Where(
                assistant => assistant.Username.Equals(username));

            return await base.Auth(assistant, password);
        }

        public override async Task<string> GenerateAccessToken(Assistant assistant)
        {
            return await base.GenerateAccessToken(assistant);
        }
    }
}