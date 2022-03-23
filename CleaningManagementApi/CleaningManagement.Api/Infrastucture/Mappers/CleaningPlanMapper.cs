using AutoMapper;
using CleaningManagement.Api.Models;
using CleaningManagement.BusinessLogic.Entity;

namespace CleaningManagement.Api.Infrastucture.Mappers
{
    public class CleaningPlanMapper : IMapper<CleaningPlanModel, CleaningPlan>
    {
        public CleaningPlan Map(CleaningPlanModel model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CleaningPlanModel, CleaningPlan>());
            var mapper = new Mapper(config);
            
            CleaningPlan cleaningPlan = mapper.Map<CleaningPlan>(model);

            return cleaningPlan;
        }
    }
}