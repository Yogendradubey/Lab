using Microsoft.EntityFrameworkCore;

namespace LabDemo.DataProvider
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {


        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<LabReport> LabReports { get; set; }

    }
}
