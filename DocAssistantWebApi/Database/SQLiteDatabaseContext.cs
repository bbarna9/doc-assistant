using System.IO;
using DocAssistantWebApi.Database.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DocAssistantWebApi.Database
{
    public sealed class SQLiteDatabaseContext : DbContext
    {
        public static string ConnectionString { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(ConnectionString); 
    }
}