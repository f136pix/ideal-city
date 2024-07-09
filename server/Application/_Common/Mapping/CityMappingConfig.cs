using System.Net.Cache;
using Application.Cities.Commands.CreateCity;
using Contracts.Cities;
using Domain.Cities;
using Mapster;

public class CityMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // maps each property to the exactly corresponding
        config.NewConfig<CreateCityRequest, CreateCityCommand>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.CountryId, src => src.CountryId)
            .Map(dest => dest.Indicators, src => src.Indicators)
            .Map(dest => dest.Weather, src => src.Weather);

        config.NewConfig<City, CityResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.CountryId, src => src.CountryId.Value.ToString());
    }
}