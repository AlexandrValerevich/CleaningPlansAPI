using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleaningManagement.Api.Controllers;
using CleaningManagement.Api.Infrastucture.Mappers;
using CleaningManagement.Api.Models;
using CleaningManagement.BusinessLogic.Entity;
using CleaningManagement.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CleaningManagement.Test.Controller
{
    public class CleaningPlansControllerTest
    {
        private readonly Mock<ICleaningPlanService> _cleaningPlanServiceMoc = new();
        private readonly IMapper<CleaningPlanModel, CleaningPlan> _maper = new CleaningPlanMapper();

        [Fact]
        public async Task GetCleaningPlans_SimpleInvoke_ShouldReturnAllCleaningPlans()
        {
            // Arrange
            IEnumerable<CleaningPlan> cleaningPlans = GetTestCleaningPlans();
            _cleaningPlanServiceMoc.Setup(_ => _.GetAllCliningPlansAsync()).ReturnsAsync(cleaningPlans);
            ICleaningPlanService cleaningPlansService = _cleaningPlanServiceMoc.Object;
            var controller = new CleaningPlansController(cleaningPlansService, _maper);

            // Act
            var actionResult = await controller.GetCleaningPlans() as OkObjectResult;
            var result = actionResult?.Value as IEnumerable<CleaningPlan>;

            // Assert
            bool isEquals = result?.Equals(cleaningPlans) ?? false;
            Assert.True(isEquals);
        }

        [Fact]
        public async Task CreateCleaningPlan_InvokeWithNotNullArgs_ShouldReturnOkObjectResult()
        {
            // Arrange
            CleaningPlanModel cleaningPlanModel = GetTestCleaningPlanModel();
            _cleaningPlanServiceMoc.Setup(_ => _.AddCleaningPlanAsync(It.IsAny<CleaningPlan>()))
                                   .ReturnsAsync(It.IsAny<CleaningPlan>());

            ICleaningPlanService cleaningPlansService = _cleaningPlanServiceMoc.Object;
            var controller = new CleaningPlansController(cleaningPlansService, _maper);

            // Act
            var actionResult = await controller.CreateCleaningPlan(cleaningPlanModel);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task GetCleaningPlansByCustomerId_InvokeWithNotNullArgs_ShouldReturnOkObjectResult()
        {
            // Arrange
            IEnumerable<CleaningPlan> cleaningPlans = GetTestCleaningPlans();
            int customerId = 123224;
            _cleaningPlanServiceMoc.Setup(_ => _.GetCliningPlansByCustomerIdAsync(It.IsAny<int>()))
                                   .ReturnsAsync(cleaningPlans);

            ICleaningPlanService cleaningPlansService = _cleaningPlanServiceMoc.Object;
            var controller = new CleaningPlansController(cleaningPlansService, _maper);

            // Act
            var actionResult = await controller.GetCleaningPlansByCustomerId(customerId);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task GetCleaningPlansByCustomerId_InvokeWithNotNullArgs_ShouldReturnCleaningPlansList()
        {
            // Arrange
            IEnumerable<CleaningPlan> cleaningPlans = GetTestCleaningPlans();
            int customerId = 123224;
            _cleaningPlanServiceMoc.Setup(_ => _.GetCliningPlansByCustomerIdAsync(It.IsAny<int>()))
                                   .ReturnsAsync(cleaningPlans);

            ICleaningPlanService cleaningPlansService = _cleaningPlanServiceMoc.Object;
            var controller = new CleaningPlansController(cleaningPlansService, _maper);

            // Act
            var actionResult = await controller.GetCleaningPlansByCustomerId(customerId) as OkObjectResult;
            var result = actionResult?.Value as IEnumerable<CleaningPlan>;

            // Assert
            bool isEquals = result?.Equals(cleaningPlans) ?? false;
            Assert.True(isEquals);
        }

        [Fact]
        public void GetCleaningPlansByCustomerId_InvokeWithNullArgs_ShouldThrowException()
        {
            // Arrange
            int? customerId = null;
            ICleaningPlanService cleaningPlansService = _cleaningPlanServiceMoc.Object;
            var controller = new CleaningPlansController(cleaningPlansService, _maper);

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => controller.GetCleaningPlansByCustomerId((int)customerId).Result);
        }

        [Fact]
        public async Task UpdateCleaningPlans_InvokeWithNotNullArgs_ShouldReturnOkResult()
        { 
            // Arrange
            CleaningPlanModel cleaningPlans = GetTestCleaningPlanModel();
            var id = new Guid();
            _cleaningPlanServiceMoc.Setup(_ => _.UpdateCleaningPlanAsync(It.IsAny<Guid>(),It.IsAny<CleaningPlan>()))
                                   .ReturnsAsync(It.IsAny<CleaningPlan>);

            ICleaningPlanService cleaningPlansService = _cleaningPlanServiceMoc.Object;
            var controller = new CleaningPlansController(cleaningPlansService, _maper);

            // Act
            var actionResult = await controller.UpdateCleaningPlans(id, cleaningPlans);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public void UpdateCleaningPlans_InvokeWithNullArgs_ShouldThrowException()
        {
            // Arrange
            CleaningPlanModel cleaningPlans = GetTestCleaningPlanModel();
            Guid? id = null;

            ICleaningPlanService cleaningPlansService = _cleaningPlanServiceMoc.Object;
            var controller = new CleaningPlansController(cleaningPlansService, _maper);

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => controller.UpdateCleaningPlans((Guid)id, cleaningPlans).Result);
        }

        [Fact]
        public async Task DeleteCleaningPlans_InvokeWithNotNullArgs_ShouldReturnOkObjectResult()
        {
            // Arrange
            var id = new Guid();
            _cleaningPlanServiceMoc.Setup(_ => _.DeleteCleningPlanAsync(It.IsAny<Guid>()))
                                   .ReturnsAsync(It.IsAny<bool>);

            ICleaningPlanService cleaningPlansService = _cleaningPlanServiceMoc.Object;
            var controller = new CleaningPlansController(cleaningPlansService, _maper);

            // Act
            var actionResult = await controller.DeleteCleaningPlans(id);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void DeleteCleaningPlans_InvokeWithNullArgs_ShouldThrowException()
        {
            // Arrange
            Guid? id = null;

            ICleaningPlanService cleaningPlansService = _cleaningPlanServiceMoc.Object;
            var controller = new CleaningPlansController(cleaningPlansService, _maper);

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => controller.DeleteCleaningPlans((Guid)id).Result);
        }
        

        private static IEnumerable<CleaningPlan> GetTestCleaningPlans() => new List<CleaningPlan>{
            new CleaningPlan
            {
                ID = new Guid(),
                Title = "Hotel Room Cleaning, double bed",
                CustomerID = 123223,
                Description = "This plan is meant to be used for double bed rooms.",
                CreationDate = CommonDateTime
            },
            new CleaningPlan
            {
                ID = new Guid(),
                Title = "Mall Cleaning, inner city",
                CustomerID = 123224,
                Description = "Suitable only for malls smaller than 23000 m².",
                CreationDate = CommonDateTime
            }
        };

        private static DateTime CommonDateTime => DateTime.Now;

        private static CleaningPlanModel GetTestCleaningPlanModel() => new()
        {
            Title = "Mall Cleaning, inner city",
            CustomerID = 123224,
            Description = "+++Suitable only for malls smaller than 23000 m².+++"
        };
    }
}