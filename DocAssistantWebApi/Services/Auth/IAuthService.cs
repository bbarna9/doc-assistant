using System.Threading.Tasks;
using DocAssistantWebApi.Database.DbModels;

namespace DocAssistantWebApi.Services.Auth
{
    public interface IAuthService
    {
        public Task<Doctor> Auth(string username, string password);
        public Task<string> GenerateAccessToken(Doctor doctor);
        public Task<bool> Authorize(string accessToken);
        public Task<bool> Logout(string accessToken);
    }
}