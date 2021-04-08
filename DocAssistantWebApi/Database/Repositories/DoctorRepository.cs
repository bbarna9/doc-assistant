using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Errors;
using Microsoft.Data.Sqlite;

namespace DocAssistantWebApi.Database.Repositories
{
    public class DoctorRepository : IRepository<Doctor>
    {
        public async Task<Doctor> Get(Doctor entity)
        {
            Doctor doctor = null;
            
            await using var ctx = new SQLiteDatabaseContext();
            
            doctor = (Doctor) await ctx.Doctors.FindAsync(entity);
            
            return doctor;
        }

        public async Task<Doctor> Where(Expression<Func<Doctor,bool>> expression)
        {
            await using var ctx = new SQLiteDatabaseContext();

            return await ctx.Doctors.FirstOrDefaultAsync(expression);
        }

        public async Task<bool> UpdateChangedProperties(Doctor entity)
        {
            await using var ctx = new SQLiteDatabaseContext();
            
            var doctor = await ctx.Doctors.FirstOrDefaultAsync(doctor => doctor.Id == entity.Id);

            var updatedProperties = IRepository<Doctor>.GetUpdatedProperties(doctor,entity);

            foreach (var property in updatedProperties)
            {
                doctor.GetType().GetProperty(property.Item1, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.NonPublic)
                    ?.SetValue(doctor,property.Item2);
            }

            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Doctor entity)
        {

            await using var ctx = new SQLiteDatabaseContext();
        
            ctx.Update(entity);

            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task Save(Doctor entity)
        {
            await using var ctx = new SQLiteDatabaseContext();

            await ctx.AddAsync(entity);
            await ctx.SaveChangesAsync();
        }
        public async Task<IEnumerable<Doctor>> WhereMulti(Expression<Func<Doctor, bool>> expression)
        {
            await using var ctx = new SQLiteDatabaseContext();

            return await ctx.Doctors.Where(expression).ToListAsync();
        }

        public Task<int> DeleteWhere(Expression<Func<Doctor, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}