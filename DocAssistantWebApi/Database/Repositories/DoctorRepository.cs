using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocAssistant_Common.Models;
using DocAssistantWebApi.Database.Factories;
using DocAssistantWebApi.Errors;
using Microsoft.Data.Sqlite;

namespace DocAssistantWebApi.Database.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>
    {
        public DoctorRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) {}
    }
}