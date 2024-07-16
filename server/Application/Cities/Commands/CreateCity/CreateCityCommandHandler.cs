using Application._Common.Interfaces;
using Domain.Cities;
using Domain.City.ValueObjects;
using Domain.CityAggregate;
using Domain.Countries;
using ErrorOr;
using MediatR;

namespace Application.Cities.Commands.CreateCity;

public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, ErrorOr<City>>
{
    private readonly ICityRepository _repository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _uow;

    public CreateCityCommandHandler(ICityRepository repository, ICountryRepository countryRepository, IUnitOfWork uow)
    {
        _repository = repository;
        _countryRepository = countryRepository;
        _uow = uow;
    }

    public async Task<ErrorOr<City>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(CountryId.Create(Guid.Parse(request.CountryId)));
        if (country is null)
        {
            return Error.NotFound(description: "Country with given UUID not found");
        }

        // create city
        var city = City.Create(
            request.Name,
            CountryId.Create(Guid.Parse(request.CountryId)),
            country,
            request.Indicators != null
                ? Indicator.Create(
                    request.Indicators.CostIndex,
                    request.Indicators.PublicTransportationIndex,
                    request.Indicators.Gasoline,
                    request.Indicators.AverageMonthlyNetSalary
                )
                : null,
            request.Weather != null
                ? Weather.Create(
                    request.Weather.AverageTemperature
                )
                : null,
            null
        );
        
        await _repository.AddAsync(city);
        await _uow.CommitAsync();
        return city;
    }
}