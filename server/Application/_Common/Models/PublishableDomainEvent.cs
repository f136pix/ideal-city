using Application._Common.Interfaces;
using Domain.Common;

namespace Application._Common.Models;

public class PublishableDomainEvent : IPublishableMessage
{
    private PublishableDomainEvent(IDomainEvent data)
    {
        Id = Guid.NewGuid();
        EventType = data.GetType().Name;
        Data = data;
    }
    
    public static PublishableDomainEvent Create (IDomainEvent @event)
    {
        return new PublishableDomainEvent(@event);
    }
    
    public Guid Id { get; }
    public string EventType { get; }
    public object Data { get; }
}