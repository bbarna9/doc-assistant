using DocAssistant_Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DocAssistantWebApi.Database
{
    public sealed class SQLiteDatabaseContext : DbContext
    {
        public static string ConnectionString { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
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
    }
}