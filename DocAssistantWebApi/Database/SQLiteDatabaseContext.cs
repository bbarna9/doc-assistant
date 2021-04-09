using System.Linq;
using System.Reflection;
using DocAssistant_Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DocAssistantWebApi.Database
{
    public sealed class SQLiteDatabaseContext : DbContext, IDatabaseContext
    {
        public static string ConnectionString { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity => entity.HasIndex(patient => patient.SSN).IsUnique());
            modelBuilder.Entity<Patient>()
                .HasIndex(patient => patient.SSN)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(ConnectionString);

        public DbSet<T> GetSet<T>() where T : class
        {
            return (DbSet<T>) this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(property =>property.PropertyType == typeof(DbSet<T>))?.GetValue(this);
        }

        public DbSet<T> GetSet<T>(string tableName) where T : class
        {
            return (DbSet<T>) this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(property =>property.PropertyType == typeof(DbSet<T>) && property.Name == tableName)?.GetValue(this);
        }

        public DbContext GetContext() => this;
    }
}