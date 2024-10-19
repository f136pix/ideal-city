namespace Contracts.Subscriptions.AddUserToSubscription;

public class AddUserToSubscriptionResponse
{
    public Guid Id { get; set; }
    public string SubscriptionType { get; set; }
    public bool IsActive { get; set; }
}