namespace Infraestructure.Common;

// public class UnitOfWork
// {
//     public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//     {
//         var domainEvents = ChangeTracker.Entries<Entity>()
//             .SelectMany(entry => entry.Entity.PopDomainEvents())
//             .ToList();
//
//         if (IsUserWaitingOnline())
//         {
//             AddDomainEventsToOfflineProcessingQueue(domainEvents);
//             return await base.SaveChangesAsync(cancellationToken);
//         }
//
//         await PublishDomainEvents(domainEvents);
//         return await base.SaveChangesAsync(cancellationToken);
//     }
// }