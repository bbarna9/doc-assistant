using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DocAssistantWebApi.Database
{
    public interface IDatabaseContext : IDisposable, IAsyncDisposable
    {
        DbContext GetContext();
        DbSet<T> GetSet<T>() where T : class;
        DbSet<T> GetSet<T>(string tableName) where T : class;
    }
}