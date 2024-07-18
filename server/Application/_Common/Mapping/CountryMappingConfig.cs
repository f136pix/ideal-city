using Application.Counties.Commands.CreateCountry;
using Application.Countries.Commands.AddCityToCountry;
using Contracts.Countries;
using Domain.Cities.Events;
using Domain.CountryAggregate;
using Mapster;
using Microsoft.AspNetCore.SignalR;

namespace Application._Common.Mapping;

public class CountryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // maps each property to the exactly corresponding
        // src -> dest
        config.NewConfig<CreateCountryRequest, CreateCountryCommand>()
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<Country, CountryResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString());
        
        config.NewConfig<CityCreated, AddCityToCountryCommand>()
            .Map(dest => dest.City, src => src.City);
        
        
    }
}