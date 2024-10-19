using Application.Cities.Commands;
using Application.Cities.Commands.CreateCity;
using Application.Cities.Commands.CreateCityReview;
using Contracts.Cities;
using Contracts.Cities.Ratings;
using Domain.CityAggregate;
using Mapster;

public class CityMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCityRequest, CreateCityCommand>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.CountryId, src => src.CountryId)
            .Map(dest => dest.Indicators, src => src.Indicators)
            .Map(dest => dest.Weather, src => src.Weather);

        config.NewConfig<City, CityResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.CountryId, src => src.CountryId.Value.ToString());

        config.NewConfig<CreateCityReviewRequest, CreateCityReviewCommand>()
            .Map(dest => dest, src => src.Adapt<CreateCityReviewCommand>());
    }
}