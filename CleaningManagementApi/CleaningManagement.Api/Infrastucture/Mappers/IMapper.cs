namespace CleaningManagement.Api.Infrastucture.Mappers
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource model);
    }
}