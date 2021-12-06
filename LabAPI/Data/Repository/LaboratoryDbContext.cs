using LaboratoryAPI.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratoryAPI.Data.Repository
{
    public class LaboratoryDbContext : DbContext
    {
        public LaboratoryDbContext(DbContextOptions<LaboratoryDbContext> db) : base(db)
        {

        }

        public DbSet<Report> Report { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<UserCredential> UserCredential { get; set; }
    }
}
