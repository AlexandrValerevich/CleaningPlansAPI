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

        public Task<IEnumerable<CleaningPlan>> GetAllCliningPlansAsync()
        {
            return _repository.ReadAllAsync();
        }
        
        public Task<IEnumerable<CleaningPlan>> GetAllCliningPlansByCustomerIdAsync(int customerId)
        {
            return _repository.GetAllCliningPlansByCustomerIdAsync(customerId);
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

        public async Task<bool> DeleteCleningPlanAsync(Guid id)
        {
            var isDeleted = await _repository.DeleteAsync(id);
            await _repository.SaveAsync();

            return isDeleted;
        }
    }
}