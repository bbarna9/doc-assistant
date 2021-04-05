using System.Threading.Tasks;

namespace DocAssistantWebApi.Database.DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(T entity);
        Task<T> GetById(long id);
        Task Update(T entity);
        Task Save(T entity);
    }
}