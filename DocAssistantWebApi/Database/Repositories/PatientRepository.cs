
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            
            patient = await ctx.Patients.FindAsync(entity);

            return patient;
        }

        public Task<bool> UpdateChangedProperties(Patient entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Patient entity)
        {
            await using var ctx = new SQLiteDatabaseContext();
            
            var patientToUpdate = await ctx.Patients.FirstOrDefaultAsync(patient => patient.Id == entity.Id);
            if (patientToUpdate == null) return false;
            
            var updatedProperties = IRepository<Patient>.GetUpdatedProperties(patientToUpdate,entity);

            foreach (var property in updatedProperties)
            {
                patientToUpdate.GetType().GetProperty(property.Item1, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.NonPublic)
                    ?.SetValue(patientToUpdate,property.Item2);
            }

            return await ctx.SaveChangesAsync() > 0;
        }
        
        public async Task Save(Patient entity)
        {
            await using var ctx = new SQLiteDatabaseContext();

            entity.ArriveTime = DateTime.Now;
            
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

            return await ctx.Patients.Where(expression)
                .OrderBy(patient => patient.ArriveTime)
                .ToListAsync();
        }

        public async Task<int> DeleteWhere(Expression<Func<Patient, bool>> expression)
        {
            await using var ctx = new SQLiteDatabaseContext();

            var patient = await ctx.Patients.FirstOrDefaultAsync(expression);

            if (patient == default) return 0;
            
            ctx.Patients.Remove(patient);
            await ctx.SaveChangesAsync();

            return 1;
        }
    }
}