using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DocAssistantWebApi.Database.Repositories
{
    public class AssistantRepository : IRepository<Assistant>
    {
        public async Task<bool> Update(Assistant entity)
        {
            await using var ctx = new SQLiteDatabaseContext();
            
            ctx.Update(entity);
            
            return await ctx.SaveChangesAsync() > 0;
        }
        
        public async Task<bool> UpdateChangedProperties(Assistant entity)
        {
            await using var ctx = new SQLiteDatabaseContext();
            
            var assistant = await ctx.Assistants.FirstOrDefaultAsync(assistant => assistant.Id == entity.Id);

            #region  Not changeable properties
            entity.DoctorId = assistant.DoctorId;
            entity.Id = assistant.Id;
            #endregion

            var updatedProperties = IRepository<Assistant>.GetUpdatedProperties(assistant,entity);

            foreach (var property in updatedProperties)
            {
                assistant.GetType().GetProperty(property.Item1, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.NonPublic)
                    ?.SetValue(assistant,property.Item2);
            }

            return await ctx.SaveChangesAsync() > 0;
        }
        
        public async Task<Assistant> Where(Expression<Func<Assistant, bool>> expression)
        {
            await using var ctx = new SQLiteDatabaseContext();

            return await ctx.Assistants.FirstOrDefaultAsync(expression);
        }
        
        #region Not required
        public Task<Assistant> Get(Assistant entity)
        {
            throw new NotImplementedException();
        }

        public Task Save(Assistant entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteWhere(Expression<Func<Assistant, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Assistant>> WhereMulti(Expression<Func<Assistant, bool>> expression)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}