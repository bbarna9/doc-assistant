using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DocAssistantWebApi.Database.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(T entity);
        Task<T> GetById(long id);
        Task Update(T entity);
        Task Save(T entity);
        Task<T> Where(Expression<Func<T, bool>> expression);
    }
}