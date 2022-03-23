using System.Linq;
using CleaningManagement.BusinessLogic.Interfaces;
using CleaningManagement.BusinessLogic.Entity;

namespace CleaningManagement.DAL.Seed
{
    public class DbInitializer
    {
        private readonly IRepository<CleaningPlan> _repository;

        public DbInitializer(IRepository<CleaningPlan> repository)
        {
            _repository = repository;
        }

        private bool IsInitialized()
        {
            var cleaningPlans = _repository.ReadAllAsync().Result;
            return cleaningPlans.Any();
        }

        public void Initialize()
        {
            if (IsInitialized())
            {
                return;
            }

            _repository.CreateAsync(new CleaningPlan
            {
                Title = "Hotel Room Cleaning, double bed",
                CustomerID = 123223,
                Description = "This plan is meant to be used for double bed rooms."
            }).Wait();

            _repository.CreateAsync(new CleaningPlan
            {
                Title = "Mall Cleaning, inner city",
                CustomerID = 123224,
                Description = "Suitable only for malls smaller than 23000 m²."
            }).Wait();

            _repository.SaveAsync().Wait();
        }

    }
}