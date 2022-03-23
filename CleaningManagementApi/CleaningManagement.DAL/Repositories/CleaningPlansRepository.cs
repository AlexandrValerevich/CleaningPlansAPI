using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CleaningManagement.BusinessLogic.Interfaces;
using CleaningManagement.BusinessLogic.Entity;

namespace CleaningManagement.DAL.Repositories
{
    public class CleaningPlansRepository : IRepository<CleaningPlan>
    {
        private readonly CleaningManagementDbContext _context;

        public CleaningPlansRepository(CleaningManagementDbContext context)
        {
            _context = context;
        }

        private DbSet<CleaningPlan> CleaningPlans => _context.CleaningPlans;

        public async Task<CleaningPlan> CreateAsync(CleaningPlan cleaningPlan)
        {
            var changeTracking = await CleaningPlans.AddAsync(cleaningPlan);
            return changeTracking.Entity;
        }

        public async Task<CleaningPlan> ReadAsync(Guid id)
        {
            return await CleaningPlans.FirstOrDefaultAsync(plan => plan.ID.Equals(id));
        }

        public Task<CleaningPlan> UpdateAsync(CleaningPlan plan)
        {
           var changeTracking = CleaningPlans.Update(plan);
           return Task.FromResult(changeTracking.Entity);
        }

        public async Task<CleaningPlan> DeleteAsync(Guid id)
        {
            CleaningPlan plan = await ReadAsync(id);
            var changeTracking = CleaningPlans.Remove(plan);

            return changeTracking.Entity;
        }

        public Task SaveAsync() => _context.SaveChangesAsync();

        public IQueryable<CleaningPlan> ReadAll() => CleaningPlans.AsQueryable();
    }
}