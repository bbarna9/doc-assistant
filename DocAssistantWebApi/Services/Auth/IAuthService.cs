using System.Threading.Tasks;
using DocAssistant_Common.Models;

namespace DocAssistantWebApi.Services.Auth
{
    public interface IAuthService
    {
        public Task<Doctor> Auth(string username, string password);
        public Task<string> GenerateAccessToken(Doctor doctor);
        public Task<(bool, long)> Authorize(string accessToken);
        public Task<bool> Logout(string accessToken);
    }
}