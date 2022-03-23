﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleaningManagement.BusinessLogic.Interfaces;
using CleaningManagement.Api.Infrastucture.Mappers;
using CleaningManagement.Api.Models;
using CleaningManagement.BusinessLogic.Entity;
using AutoMapper;

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
        public IActionResult GetCleaningPlans()
        {
            var cleaningPlans = _cleaningPlanService.GetAllCliningPlans();
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
        public IActionResult GetCleaningPlansByCustomerId(int customerId)
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
                var deletedCliningPlan = await _cleaningPlanService.DeleteCleningPlanAsync(id);
                return Ok(deletedCliningPlan);
            }

            return BadRequest();
        }
    }
}