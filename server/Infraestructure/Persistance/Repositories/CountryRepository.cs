using Application._Common.Interfaces;
using Domain.Countries;
using Domain.CountryAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistance.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly IdealCityDbContext _dbContext;

    public CountryRepository(IdealCityDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Country> GetByIdAsync(CountryId id)
    {
        return _dbContext.Countries
            .Include(c => c.Cities)
            .Include(c => c.CityIds)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Country GetByIdSync(CountryId id)
    {
        throw new NotImplementedException();
    }

    public Task<Country> DeleteAsync(CountryId id)
    {
        throw new NotImplementedException();
    }

    public Task<Country> GetByProperty(string propertyName, string value)
    {
        switch (propertyName)
        {
            case "Name":
                return _dbContext.Countries
                    .Include(c => c.Cities)
                    .Include(c => c.CityIds)
                    .FirstOrDefaultAsync(c => c.Name == value);
            default:
                throw new ArgumentException("Invalid property name");
        }
        
    }

    public Task<IReadOnlyList<Country>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Country> AddAsync(Country entity)
    {
        throw new NotImplementedException();
    }

    public Task<Country> UpdateAsync(Country entity)
    {
        throw new NotImplementedException();
    }
}