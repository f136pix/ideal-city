namespace Domain.Common;

public abstract class Entity<TGuid>
    where TGuid : notnull
{
    public TGuid Id { get; private init; }

    protected readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    protected Entity(TGuid id)
    {
        Id = id;
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