using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleaningManagement.BusinessLogic.Interfaces;
using CleaningManagement.BusinessLogic.Entity;
using CleaningManagement.BusinessLogic.Services;
using Moq;
using Xunit;

namespace CleaningManagement.Test.Services
{
    public class CleaningPlansServiceTest
    {
        private readonly Mock<IRepository<CleaningPlan>> _repositoryMoc = new();

        private IRepository<CleaningPlan> Repository => _repositoryMoc.Object;

        [Fact]
        public async Task AddCleaningPlanAsync_InvokeWithNotNullArg_ShouldReturnAddedItem()
        {
            // Arrange
            CleaningPlan cleaningPlan = GetSingleCleaningPlan();
            _repositoryMoc.Setup(_ => _.CreateAsync(It.IsAny<CleaningPlan>()))
                          .ReturnsAsync(cleaningPlan);

            var cleaningPlanService = GetCleaningPlanService();

            // Act
            var result = await cleaningPlanService.AddCleaningPlanAsync(cleaningPlan);

            // Assert;
            bool isEquals = result.Equals(cleaningPlan);
            Assert.True(isEquals);
        }

        [Fact]
        public async Task GetAllCliningPlansAsync_Invoke_ShouldReturnAllPlans()
        {
            // Arrange
            IEnumerable<CleaningPlan> cleaningPlans = GetTestCleaningPlans();
            _repositoryMoc.Setup(_ => _.ReadAllAsync()).ReturnsAsync(cleaningPlans);
            var cleaningPlanService = GetCleaningPlanService();

            // Act
            IEnumerable<CleaningPlan> result = await cleaningPlanService.GetAllCliningPlansAsync();

            // Assert
            bool isEquals = result.Equals(cleaningPlans);
            Assert.True(isEquals);
        }

        [Fact]
        public async Task GetCliningPlansByCustomerIdAsync_InvokeWithNotNullArg_ShouldReturnPlans()
        {
            // Arrange
            IEnumerable<CleaningPlan> cleaningPlans = GetTestCleaningPlans();
            int customerId = 123224;
            _repositoryMoc.Setup(_ => _.GetCliningPlansByCustomerIdAsync(customerId))
                          .ReturnsAsync(cleaningPlans);

            CleaningPlanService cleaningPlanService = GetCleaningPlanService();

            // Act
            var result = await cleaningPlanService.GetCliningPlansByCustomerIdAsync(customerId);

            // Assert
            var isEquals = result.Equals(cleaningPlans);
            Assert.True(isEquals);
        }

        [Fact]
        public async Task GetCliningPlansByCustomerIdAsync_InvokeWithNotExistingCustomer_ShouldReturnEmptyArray()
        {
            // Arrange
            IEnumerable<CleaningPlan> cleaningPlans = new List<CleaningPlan>();
            int customerId = 123224;
            _repositoryMoc.Setup(_ => _.GetCliningPlansByCustomerIdAsync(customerId))
                          .ReturnsAsync(cleaningPlans);

            CleaningPlanService cleaningPlanService = GetCleaningPlanService();

            // Act
            var result = await cleaningPlanService.GetCliningPlansByCustomerIdAsync(customerId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateCleaningPlanAsync_InvokeWithNotNullArg_ShouldReturnUpdatedPlan()
        {
            // Arrange
            CleaningPlan cleaningPlan = GetSingleCleaningPlan();
            Guid id = cleaningPlan.ID;
            _repositoryMoc.Setup(_ => _.ReadAsync(id)).ReturnsAsync(cleaningPlan);
            _repositoryMoc.Setup(_ => _.UpdateAsync(cleaningPlan));

            CleaningPlanService cleaningPlanService = GetCleaningPlanService();

            // Act
            var result = await cleaningPlanService.UpdateCleaningPlanAsync(id, cleaningPlan);

            // Assert
            bool isEquals = result.Equals(cleaningPlan);
            Assert.True(isEquals);
        }

        [Fact]
        public async Task DeleteCleningPlanAsync_InvokeWithNotNullArg_ShouldReturnBool()
        {
            // Arrange
            var id = new Guid();
            _repositoryMoc.Setup(_ => _.DeleteAsync(id)).ReturnsAsync(It.IsAny<bool>());

            CleaningPlanService cleaningPlanService = GetCleaningPlanService();

            // Act  
            var result = await cleaningPlanService.DeleteCleningPlanAsync(id);

            // Assert 
            Assert.IsType<bool>(result);
        }

        private CleaningPlanService GetCleaningPlanService() => new(Repository);

        private static IEnumerable<CleaningPlan> GetTestCleaningPlans() => new List<CleaningPlan>{
            new CleaningPlan
            {
                ID = new Guid(),
                Title = "Hotel Room Cleaning, double bed",
                CustomerID = 123223,
                Description = "This plan is meant to be used for double bed rooms.",
            },
            new CleaningPlan
            {
                ID = new Guid(),
                Title = "Mall Cleaning, inner city",
                CustomerID = 123224,
                Description = "Suitable only for malls smaller than 23000 m².",
            }
        };

        private static CleaningPlan GetSingleCleaningPlan() => new()
        {
            ID = new Guid(),
            Title = "Mall Cleaning, inner city---",
            CustomerID = 123224,
            Description = "Suitable only for malls smaller than 23000 m².---",
        };
    }
}