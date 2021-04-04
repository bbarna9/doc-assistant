using System.IO;
using DocAssistantWebApi.Database.DbModels;
using Microsoft.EntityFrameworkCore;
using Patient = DocAssistant_Common.Models.Patient;

namespace DocAssistantWebApi.Database
{
    public sealed class SQLiteDatabaseContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        private readonly string connectionString; // @"Data Source=" + Directory.GetCurrentDirectory() + "/healthcare.db"
        
        public SQLiteDatabaseContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(this.connectionString); 
    }
}