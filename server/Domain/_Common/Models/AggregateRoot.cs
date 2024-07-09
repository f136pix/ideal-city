namespace Domain.Common;

public abstract class AggregateRoot<TGuid> : Entity<TGuid>
    where TGuid : notnull
{
    protected AggregateRoot(TGuid id) : base(id)
    {
    }

    // parameterless ctor for ef
#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618
    
}