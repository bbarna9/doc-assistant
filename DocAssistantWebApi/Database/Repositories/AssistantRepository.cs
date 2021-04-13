using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocAssistant_Common.Models;
using System.Linq;
using System.Reflection;
using DocAssistantWebApi.Database.Factories;
using Microsoft.EntityFrameworkCore;
using AssistantModel = DocAssistant_Common.Models.Assistant;

namespace DocAssistantWebApi.Database.Repositories.Assistant
{
    public class AssistantRepository : BaseRepository<AssistantModel>,IAssistantRepository<AssistantModel>
    {
        public AssistantRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        public override async Task<bool> Update(AssistantModel entity)
        {
            await using var ctx = new SQLiteDatabaseContext();
            
            ctx.Update(entity);
            
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}