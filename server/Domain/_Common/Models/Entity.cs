namespace Domain.Common;

public abstract class Entity<TGuid> : IHasDomainEvents
    where TGuid : notnull
{
    public TGuid Id { get; private init; }
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity(TGuid id)
    {
        Id = id;
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TGuid> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TGuid>? other)
    {
        return Equals((object?)other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity<TGuid> left, Entity<TGuid> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TGuid> left, Entity<TGuid> right)
    {
        return !(left == right);
    }

    // parameterless ctor for ef 
#pragma warning disable CS8618
    protected Entity()
    {
    }
#pragma warning restore CS8618
}