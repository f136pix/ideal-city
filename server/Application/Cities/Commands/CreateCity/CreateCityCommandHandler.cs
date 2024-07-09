using Application._Common.Interfaces;
using Domain.Cities;
using Domain.City.ValueObjects;
using Domain.Countries;
using ErrorOr;
using MediatR;

namespace Application.Cities.Commands.CreateCity;

public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, ErrorOr<City>>
{
    private readonly ICommonRepository<City> _repository;

    public CreateCityCommandHandler(ICommonRepository<City> repository)
    {
        _repository = repository;
    }
    
    public async Task<ErrorOr<City>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        // create city
        var city = City.Create(
                request.Name,
                CountryId.Create(request.CountryId),
                request.Indicators != null ? Indicator.Create(
                    request.Indicators.CostIndex,
                    request.Indicators.PublicTransportationIndex,
                    request.Indicators.Gasoline,
                    request.Indicators.AverageMonthlyNetSalary
                ) : null,
                request.Weather != null ? Weather.Create(
                    request.Weather.AverageTemperature
                ) : null,
                null
            );
        
        // persist
        await _repository.AddAsync(city);

        city = await _repository.GetByIdAsync("c6b73d11-2e7e-4419-85a1-c488b2a46a61");
        
        Console.WriteLine(city.Country.CityIds.Count);
        Console.WriteLine(city.Country.Cities.FirstOrDefault().Name);
        
        return city;
    }
}