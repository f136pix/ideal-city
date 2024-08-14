using ErrorOr;

namespace Domain.UserAggregate.Errors;

public static class SubscriptionErrors
{
    public static readonly Error UserIsAlreadyOnSubscription = Error
        .Conflict(
            code: "User.Is.Already.On.Subscription",
            description: "User is already on subscription"
        );
    
    public static readonly Error UserIsNotOnSubscription = Error
        .Conflict(
            code: "User.Is.Not.On.Subscription",
            description: "User is not on subscription"
        );

    public static readonly Error SubscriptionIsNotActive = Error
        .Conflict(
            code: "Subscription.Is.Not.Active",
            description: "Subscription is not active"
        );


    public static readonly Error CannotHaveMoreUsersThanTheSubscriptionAllows = Error
        .Conflict(
            code: "Subscription.Cannot.Have.More.Users.Than.Allowed",
            description: "Subscription is full"
        );
}