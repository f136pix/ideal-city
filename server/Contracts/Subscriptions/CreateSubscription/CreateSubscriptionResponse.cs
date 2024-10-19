namespace Contracts.Subscriptions;

public class CreateSubscriptionResponse
{
    public Guid Id { get; set; }
    public string SubscriptionType { get; set; }
    public bool IsActive { get; set; }
}