using Domain.City.ValueObjects;
using Domain.Common;
using Domain.Countries;

namespace Domain.CountryAggregate;

public class Country : AggregateRoot<CountryId>
{
    private readonly List<CityAggregate.City> _cities = new();
    private List<CityId> _cityIds => GetCityIds();
    public string Name { get; private set; }
    public IReadOnlyList<CityAggregate.City> Cities => _cities.AsReadOnly();
    public IReadOnlyList<CityId> CityIds => _cityIds.AsReadOnly();

    private Country(CountryId id, string name) : base(id)
    {
        Name = name;
    }

    public static Country Create(string name)
    {
        var country = new Country(CountryId.CreateUnique(), name);

        // pushes domain event
        // country._domainEvents.Add();

        return country;
    }
    
    private List<CityId> GetCityIds()
    {
        var cityIds =
            from city in Cities
            select city.Id;

        return cityIds.ToList();
    }

#pragma warning disable CS8618
    private Country()
    {
    }
#pragma warning restore CS8618
}