using Application._Common.Interfaces;
using Domain.Cities;
using Domain.CityAggregate;
using Domain.CountryAggregate;
using ErrorOr;
using MediatR;

namespace Application.Countries.Commands.AddCityToCountry;

public class AddCityToCountryCommandHandler : IRequestHandler<AddCityToCountryCommand, ErrorOr<Country>>
{
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _uow;

    public AddCityToCountryCommandHandler(ICityRepository cityRepository, ICountryRepository countryRepository,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
        _uow = unitOfWork;
    }

    public async Task<ErrorOr<Country>> Handle(AddCityToCountryCommand request, CancellationToken cancellationToken)
    {
        City city = request.City;
        city.SetCountry(city.CountryId);
        city.Country.AddCityId(city.Id); // adds the city to the country helper table that keeps tracks of it CitiesIds

        await _uow.CommitAsync();

        return city.Country;
    }
}