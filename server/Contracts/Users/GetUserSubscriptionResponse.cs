namespace Contracts.Users;

public record GetUserSubscriptionResponse
{
    public string Id { get; set; }
    public string SubscriptionType { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
}