using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleaningManagement.BusinessLogic.Entity;

namespace CleaningManagement.BusinessLogic.Interfaces
{
    public interface ICleaningPlanService
    {
        Task<CleaningPlan> AddCleaningPlanAsync(CleaningPlan plan);
        Task<IEnumerable<CleaningPlan>> GetAllCliningPlansAsync();
        Task<IEnumerable<CleaningPlan>> GetCliningPlansByCustomerIdAsync(int customerId);
        Task<CleaningPlan> UpdateCleaningPlanAsync(Guid updatedPlanId, CleaningPlan plan);
        Task<bool> DeleteCleningPlanAsync(Guid id);
    }
}