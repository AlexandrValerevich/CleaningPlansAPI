using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleaningManagement.BusinessLogic.Interfaces;
using CleaningManagement.Api.Infrastucture.Mappers;
using CleaningManagement.Api.Models;
using CleaningManagement.BusinessLogic.Entity;

namespace CleaningManagement.Api.Controllers
{
    [ApiController]
    [Route("api/cleaningplans")]
    public class CleaningPlansController : ControllerBase
    {
        private readonly ICleaningPlanService _cleaningPlanService;
        private readonly IMapper<CleaningPlanModel, CleaningPlan> _maper;

        public CleaningPlansController(ICleaningPlanService cleaningPlanService,
                                       IMapper<CleaningPlanModel, CleaningPlan> maper)
        {
            _cleaningPlanService = cleaningPlanService;
            _maper = maper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCleaningPlans()
        {
            var cleaningPlans = await _cleaningPlanService.GetAllCliningPlansAsync();
            return Ok(cleaningPlans);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCleaningPlan(CleaningPlanModel model)
        {
            if (ModelState.IsValid)
            {
                CleaningPlan addedCleaningPlan = _maper.Map(model);
                CleaningPlan createdCleaningPlan = await _cleaningPlanService.AddCleaningPlanAsync(addedCleaningPlan);

                return Ok(createdCleaningPlan);
            }
            return BadRequest();
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCleaningPlansByCustomerId(int customerId)
        {
            if (ModelState.IsValid)
            {
                var cleaningPlans = await _cleaningPlanService.GetAllCliningPlansByCustomerIdAsync(customerId);
                return Ok(cleaningPlans);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCleaningPlans(Guid id, [FromBody]CleaningPlanModel model)
        {
            if (ModelState.IsValid)
            {
                CleaningPlan updatedCleaningPlan = _maper.Map(model);
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
                bool isDeleted = await _cleaningPlanService.DeleteCleningPlanAsync(id);
                return Ok(isDeleted);
            }

            return BadRequest();
        }
    }
}