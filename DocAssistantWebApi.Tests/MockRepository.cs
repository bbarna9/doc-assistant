using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocAssistantWebApi.Database.Repositories;

namespace DocAssistantWebApi.Tests
{
    public class MockRepository<T> : IRepository<T> where T : class
    {
        public Task<T> Get(T entity)
        {
            return Task.FromResult(entity);
        }

        public Task<bool> UpdateChangedProperties(T entity)
        {
            return Task.FromResult(true);
        }

        public Task<bool> Update(T entity)
        {
            return Task.FromResult(true);
        }

        public Task<bool> Save(T entity)
        {
            return Task.FromResult(true);
        }

        public Task<int> DeleteWhere(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(1);
        }

        public Task<T> Where(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult<T>(null);
        }

        public Task<IEnumerable<T>> WhereMulti(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult<IEnumerable<T>>(null);
        }
    }
}