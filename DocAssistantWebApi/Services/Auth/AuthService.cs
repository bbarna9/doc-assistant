using System.Threading.Tasks;
using DocAssistantWebApi.Database.DbModels;
using DocAssistantWebApi.Database.Repositories;
using DocAssistantWebApi.Utils;

namespace DocAssistantWebApi.Services
{
    public class Session : ISession
    {
        private readonly IRepository<Doctor> _doctorRepository;
        
        public Session(IRepository<Doctor> doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }
        public async Task<bool> Auth(string username, string password)
        {
            Doctor doctor = await this._doctorRepository.Where(
                doctor => doctor.Username.Equals(username));

            return SecurityUtils.VerifyPassword(password, doctor.Password);
        }
    }
}