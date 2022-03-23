using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleaningManagement.BusinessLogic.Entity;

namespace CleaningManagement.BusinessLogic.Interfaces
{
    public interface ICleaningPlanService
    {
        Task<CleaningPlan> AddCleaningPlanAsync(CleaningPlan plan);
        IEnumerable<CleaningPlan> GetAllCliningPlans();
        IEnumerable<CleaningPlan> GetAllCliningPlansByCustomerId(int customerId);
        Task<CleaningPlan> UpdateCleaningPlanAsync(Guid updatedPlanId, CleaningPlan plan);
        Task<IEnumerable<CleaningPlan>> DeleteCleningPlanAsync(Guid id);
    }
}