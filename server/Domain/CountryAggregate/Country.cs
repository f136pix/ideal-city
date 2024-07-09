using Domain.Cities;
using Domain.City.ValueObjects;
using Domain.Common;
using Domain.Countries;

public class Country : AggregateRoot<CountryId>
{
    
    private readonly List<City> _cities = new();
    private readonly List<CityId> _cityIds = new();

    public string Name { get; private set; }

    public IReadOnlyList<City> Cities => _cities.AsReadOnly();
    public IReadOnlyList<CityId> CityIds => _cityIds.AsReadOnly();

    private Country(CountryId id, string name, List<CityId> citiesIds) : base(id)
    {
        Name = name;
        _cityIds = citiesIds;
    }

    public static Country Create(string name, List<CityId> citiesIds)
    {
        var country = new Country(CountryId.CreateUnique(), name, citiesIds);

        // pushes domain event
        // country._domainEvents.Add();

        return country;
    }

#pragma warning disable CS8618
    private Country()
    {
    }
#pragma warning restore CS8618
}