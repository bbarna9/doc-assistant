using System.Threading.Tasks;
using DocAssistantWebApi.Database.DbModels;

namespace DocAssistantWebApi.Database.DataAccess
{
    public class DoctorDataAccess : IDataAccess<Doctor>
    {
        public Task<Doctor> Get(Doctor entity)
        {
            Patient patient = null;
            
            await using var ctx = new SQLiteDatabaseContext(this.connectionString);
            
            patient = (Patient) await ctx.Patients.FindAsync(entity);

            return patient;
        }

        public Task<Doctor> GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Doctor entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Save(Doctor entity)
        {
            throw new System.NotImplementedException();
        }
    }
}