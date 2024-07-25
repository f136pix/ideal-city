using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infraestructure.Persistance.Interceptors;

public class SaveTimeStampsInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        SaveTimeStamps(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }


    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken()
    )
    {
        await SaveTimeStamps(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private Task SaveTimeStamps(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return Task.CompletedTask;
        }

        // Gets all IHasTimeStamp Created || Updated
        var entries = dbContext.ChangeTracker.Entries()
            .Where(e => e.Entity is IHasTimeStamps &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entity in entries)
        {
            ((IHasTimeStamps)entity.Entity).SetCreatedAtNow();

            if (entity.State == EntityState.Added)
            {
                ((IHasTimeStamps)entity.Entity).SetCreatedAtNow();
            }
        }

        return Task.CompletedTask;
    }
}