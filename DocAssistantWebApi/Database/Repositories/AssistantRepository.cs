using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocAssistant_Common.Models;

namespace DocAssistantWebApi.Database.Repositories
{
    public class AssistantRepository : IRepository<Assistant>
    {
        public Task<Assistant> Get(Assistant entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateChangedProperties(Assistant entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Assistant entity)
        {
            throw new NotImplementedException();
        }

        public Task Save(Assistant entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWhere(Expression<Func<Assistant, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Assistant> Where(Expression<Func<Assistant, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Assistant>> WhereMulti(Expression<Func<Assistant, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}