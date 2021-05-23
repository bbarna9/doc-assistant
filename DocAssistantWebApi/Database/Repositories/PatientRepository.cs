
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Factories;
using Microsoft.EntityFrameworkCore;

namespace DocAssistantWebApi.Database.Repositories
{
    public class PatientRepository : BaseRepository<Patient>
    {
        public PatientRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) {}

        public override async Task<Patient> Where(Expression<Func<Patient,bool>> expression)
        {
            await using var ctx = this.DatabaseFactory.Create();

            return await ctx.GetSet<Patient>()
                .Include(patient => patient.Diagnoses)
                .FirstOrDefaultAsync(expression);
        }
        
        public override async Task<bool> Save(Patient entity)
        {
            await using var ctx = this.DatabaseFactory.Create();

            entity.ArriveTime = DateTime.Now;
            
            await ctx.GetContext().AddAsync(entity);
            return await ctx.GetContext().SaveChangesAsync() > 0;
        }

        public override async Task<IEnumerable<Patient>> WhereMulti(Expression<Func<Patient, bool>> expression)
        {
            await using var ctx = this.DatabaseFactory.Create();

            return await ctx.GetSet<Patient>().Where(expression)
                .Include(patient => patient.Diagnoses)
                .OrderBy(patient => patient.ArriveTime)
                .ToListAsync();
        }

        public override Task<bool> UpdateChangedProperties(Patient entity, Expression<Func<Patient, bool>> expression = null)
        {
            return base.UpdateChangedProperties(entity, patient => patient.PatientId == entity.PatientId);
        }
    }
}