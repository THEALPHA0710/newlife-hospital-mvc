using Microsoft.EntityFrameworkCore;

namespace NewLifeHospital.Models
{
    public class PatientInfoDbContext : DbContext
    {
        public DbSet<PatientInfoDetail> PatientInfoDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\MSSQLLocalDB;Database=NewLifeHospitalDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}
