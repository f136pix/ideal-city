using Contracts.Subscriptions;
using Domain.Common;

namespace Application._Common.Authorization;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeAttribute : Attribute
{
    // public string? Permissions { get; set; }
    // public string? Roles { get; set; }
    public SubscriptionTypeEnum Subscription { get; set; }
}