using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infraestructure.Persistance.Interceptors;

// Overrides the SaveChanges and SaveChangesAsync methods to capture and execute the published domain events
public class PublishDomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _mediator;

    public PublishDomainEventInterceptor(IPublisher mediator)
    {
        _mediator = mediator;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken()
    )
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return;
        }

        // GET ALL ENTITIES
        var entitiesWithDomainEvents = dbContext
            .ChangeTracker.Entries<IHasDomainEvents>() // GET ALL IHASDOMAINEVENTS UPDATED ENTITIES
            .Where(entry => entry.Entity.DomainEvents.Any()).ToList()
            .Select(entry => entry.Entity)
            .ToList();

        // GET ALL DOMAIN EVENTS QUEUED
        var domainEvents = entitiesWithDomainEvents
            .SelectMany(entity => entity.DomainEvents)
            .ToList();

        // CLEAR DOMAIN EVENTS
        foreach (IHasDomainEvents entity in entitiesWithDomainEvents)
        {
            entity.ClearDomainEvents();
        }

        // PUBLISH THEM ALL
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);   
        }
    }
}