
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

        public override async Task Save(Patient entity)
        {
            await using var ctx = this.DatabaseFactory.Create();

            entity.ArriveTime = DateTime.Now;
            
            await ctx.GetContext().AddAsync(entity);
            await ctx.GetContext().SaveChangesAsync();
        }

        public override async Task<IEnumerable<Patient>> WhereMulti(Expression<Func<Patient, bool>> expression)
        {
            await using var ctx = this.DatabaseFactory.Create();

            return await ctx.GetSet<Patient>().Where(expression)
                .OrderBy(patient => patient.ArriveTime)
                .ToListAsync();
        }
    }
}