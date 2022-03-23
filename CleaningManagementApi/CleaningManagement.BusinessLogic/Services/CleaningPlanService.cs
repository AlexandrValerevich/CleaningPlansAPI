using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleaningManagement.BusinessLogic.Interfaces;
using CleaningManagement.BusinessLogic.Entity;

namespace CleaningManagement.BusinessLogic.Services
{
    public class CleaningPlanService : ICleaningPlanService
    {
        private readonly IRepository<CleaningPlan> _repository;

        public CleaningPlanService(IRepository<CleaningPlan> repository)
        {
            _repository = repository;
        }

        public async Task<CleaningPlan> AddCleaningPlanAsync(CleaningPlan plan)
        {
            CleaningPlan addedPlan = await _repository.CreateAsync(plan);
            await _repository.SaveAsync();

            return addedPlan;
        }

        public IEnumerable<CleaningPlan> GetAllCliningPlans()
        {
            return _repository.ReadAll();
        }
        
        public IEnumerable<CleaningPlan> GetAllCliningPlansByCustomerId(int customerId)
        {
            return _repository.ReadAll().Where(plan => plan.CustomerID.Equals(customerId));
        }

        public async Task<CleaningPlan> UpdateCleaningPlanAsync(Guid updatedPlanId, CleaningPlan plan)
        {
            CleaningPlan updatedPlan = await _repository.ReadAsync(updatedPlanId);

            updatedPlan.Title = plan.Title;
            updatedPlan.CustomerID = plan.CustomerID;
            updatedPlan.Description = plan.Description;

            await _repository.UpdateAsync(updatedPlan);
            await _repository.SaveAsync();

            return updatedPlan;
        }

        public async Task<IEnumerable<CleaningPlan>> DeleteCleningPlanAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();

            var cleaningPlans = _repository.ReadAll();
            return cleaningPlans;
        }

    }
}