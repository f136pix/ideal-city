namespace Domain.Common;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        return Equals((object)other);
    }
    
    public override bool Equals(object? obj)
    {
        // checks if both obj are same type
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        // cast to ValueObject
        ValueObject other = (ValueObject)obj;

        // compares equality components
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
    
    public T Select<T>(Func<ValueObject, T> selector)
    {
        return selector(this);
    }
}