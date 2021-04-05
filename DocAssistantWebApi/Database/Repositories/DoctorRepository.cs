using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocAssistantWebApi.Database.DbModels;
using Microsoft.EntityFrameworkCore;

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

            return await ctx.Doctors.Where(expression).FirstOrDefaultAsync();
        }

        public Task<Doctor> GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Doctor entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task Save(Doctor entity)
        {
            await using var ctx = new SQLiteDatabaseContext();
            
            await ctx.AddAsync(entity);
            await ctx.SaveChangesAsync();
        }
    }
}