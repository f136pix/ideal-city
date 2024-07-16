using Domain.Cities;
using Domain.City.ValueObjects;
using Domain.CityAggregate;

namespace Application._Common.Interfaces;

public interface ICityRepository : IRepository<City>
{
    Task<City> GetByIdAsync(CityId id);
    City GetByIdSync(CityId id);
    Task<City> DeleteAsync(CityId id);
    public Task<City> GetByProperty(string propertyName, string value);
}