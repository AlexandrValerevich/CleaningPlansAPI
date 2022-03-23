using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleaningManagement.BusinessLogic.Interfaces;
using CleaningManagement.Api.Models;
using CleaningManagement.BusinessLogic.Entity;

namespace CleaningManagement.Api.Controllers
{
    [ApiController]
    [Route("api/cleaningplans")]
    public class CleaningPlansController : ControllerBase
    {
        private readonly ICleaningPlanService _cleaningPlanService;

        public CleaningPlansController(ICleaningPlanService cleaningPlanService)
        {
            _cleaningPlanService = cleaningPlanService;
        }

        [HttpGet]
        public IActionResult GetCleaningPlans()
        {
            var cleaningPlans = _cleaningPlanService.GetAllCliningPlans();
            return Ok(cleaningPlans);
        }

        [HttpPost]
        public async Task<IActionResult> GetActionResult(CleaningPlanModel model)
        {
            if (ModelState.IsValid)
            {
                CleaningPlan addedCleaningPlan = ConvertModelToCleaningPlan(model);
                CleaningPlan createdCleaningPlan = await _cleaningPlanService.AddCleaningPlanAsync(addedCleaningPlan);

                return Ok(createdCleaningPlan);
            }
            return BadRequest();
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCustomersCleaningPlans(int customerId)
        {
            if (ModelState.IsValid)
            {
                var cleaningPlans = _cleaningPlanService.GetAllCliningPlansByCustomerId(customerId);
                return Ok(cleaningPlans);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCleaningPlans(Guid id, [FromBody]CleaningPlanModel model)
        {
            if (ModelState.IsValid)
            {
                CleaningPlan updatedCleaningPlan = ConvertModelToCleaningPlan(model);
                await _cleaningPlanService.UpdateCleaningPlanAsync(id, updatedCleaningPlan);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCleaningPlans(Guid id)
        {
            if (ModelState.IsValid)
            {
                var deletedCliningPlan = await _cleaningPlanService.DeleteCleningPlanAsync(id);
                return Ok(deletedCliningPlan);
            }

            return BadRequest();
        }


        private static CleaningPlan ConvertModelToCleaningPlan(CleaningPlanModel model) => new()
        {
            Title = model.Title,
            CustomerID = model.CustomerID,
            Description = model.Description
        };

    }
}