using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using DocAssistant_Common.Attributes;

namespace DocAssistantWebApi.Database.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(T entity);
        Task<bool> UpdateChangedProperties(T entity);
        Task<bool> Update(T entity);
        Task<bool> Save(T entity);
        Task<int> DeleteWhere(Expression<Func<T, bool>> expression);
        Task<T> Where(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> WhereMulti(Expression<Func<T, bool>> expression);
        
        public static IEnumerable<(string, object)> GetUpdatedProperties(T entity)
        {
            var updatedProperties = new List<(string, object)>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                var value = property.GetValue(entity);
                if (value != default && property.GetCustomAttributes().FirstOrDefault(attribute => attribute is FixedAttribute) == null)
                {
                    updatedProperties.Add((property.Name,value));
                }
            }

            return updatedProperties;
        }
        public static IEnumerable<(string, object)> GetUpdatedProperties(T original,T updated)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase |
                                                     BindingFlags.Instance | BindingFlags.NonPublic);
            var updatedProperties = new List<(string, object)>();

            foreach (var property in properties)
            {
                var originalValue = property.GetValue(original);
                var updateValue = property.GetValue(updated);

                if (updateValue != default && originalValue != updateValue && property.GetCustomAttributes().FirstOrDefault(attribute => attribute is FixedAttribute) == null)
                {
                    updatedProperties.Add((property.Name,updateValue));
                }
            }

            return updatedProperties;
        }
    }
}