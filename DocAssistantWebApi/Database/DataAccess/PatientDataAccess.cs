
using System;
using System.Threading.Tasks;
using DocAssistantWebApi.Database.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DocAssistantWebApi.Database.DataAccess
{
    public class PatientDataAccess : IDataAccess<Patient>
    {
        private readonly string connectionString;
        
        public PatientDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public async Task<Patient> Get(Patient entity)
        {
            Patient patient = null;
            using (var ctx = new SQLiteDatabaseContext(this.connectionString))
            {
                patient = (Patient) await ctx.Patients.FindAsync(entity);
            }

            return patient;
        }

        public async Task<Patient> GetById(long id)
        {
            Patient patient = null;
            using (var ctx = new SQLiteDatabaseContext(this.connectionString))
            {
                patient = (Patient) await ctx.Patients.FindAsync(new Patient{ Id = id});
            }

            return patient;
        }

        public async Task Update(Patient entity)
        {
          //  using (var ctx = new SQLiteDatabaseContext(this.connectionString))
              //  await ctx.Patients.Update(entity);
              throw new NotImplementedException();
        }
        
        public async Task Save(Patient entity)
        {
            using (var ctx = new SQLiteDatabaseContext(this.connectionString))
                await ctx.Patients.AddAsync(entity);
        }
    }
}