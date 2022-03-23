using Microsoft.EntityFrameworkCore;
using CleaningManagement.BusinessLogic.Entity;

namespace CleaningManagement.DAL
{
    public class CleaningManagementDbContext : DbContext
    {
        public string DbPath { get; }

        public CleaningManagementDbContext()
        {
        }

        public DbSet<CleaningPlan> CleaningPlans { get; set; }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseInMemoryDatabase("CleaningContext");
    }
}