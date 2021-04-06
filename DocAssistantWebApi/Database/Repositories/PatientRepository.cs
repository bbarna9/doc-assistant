
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
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

        public Task UpdateChangedProperties(Patient entity)
        {
            throw new NotImplementedException();
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
            await using var ctx = new SQLiteDatabaseContext();

            await ctx.AddAsync(entity);
            await ctx.SaveChangesAsync();
        }

        public Task Patch(Patient entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Patient> Where(Expression<Func<Patient,bool>> expression)
        {
            await using var ctx = new SQLiteDatabaseContext();

            return await ctx.Patients.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Patient>> WhereMulti(Expression<Func<Patient, bool>> expression)
        {
            await using var ctx = new SQLiteDatabaseContext();

            return await ctx.Patients.Where(expression).ToListAsync();
        }
    }
}