using Microsoft.EntityFrameworkCore;
using LabAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<PatientDetails> PatientDetails { get; set; }
        public DbSet<LabTests> LabTests { get; set; }
        public DbSet<TestResult> Results { get; set; }

    }
}
