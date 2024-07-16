using Application._Common.Interfaces;

namespace Infraestructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly IdealCityDbContext _context;

    public UnitOfWork(IdealCityDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}