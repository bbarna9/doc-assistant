
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocAssistantWebApi.Database.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DocAssistantWebApi.Database.Repositories
{
    public class PatientRepository : IRepository<Patient>
    {
        public async Task<Patient> Get(Patient entity)
        {
            Patient patient = null;
            
            await using var ctx = new SQLiteDatabaseContext();
            
            patient = (Patient) await ctx.Patients.FindAsync(entity);

            return patient;
        }

        public async Task<Patient> GetById(long id)
        {
            Patient patient = null;
            
            await using var ctx = new SQLiteDatabaseContext();
            
            patient = (Patient) await ctx.Patients.FindAsync(new Patient{ Id = id});

            return patient;
        }

        public async Task Update(Patient entity)
        {
            await using var ctx = new SQLiteDatabaseContext();
            
            var patientToUpdate = await ctx.Patients.FirstOrDefaultAsync();
            if (patientToUpdate != null)
            {
                patientToUpdate.FirstName = entity.FirstName;
                patientToUpdate.LastName = entity.LastName;

                    
                await ctx.SaveChangesAsync();
            }
        }
        
        public async Task Save(Patient entity)
        {
            using (var ctx = new SQLiteDatabaseContext())
                await ctx.Patients.AddAsync(entity);
        }
        
        public async Task<Patient> Where(Expression<Func<Patient,bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}