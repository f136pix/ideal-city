using Application._Common.Authorization;
using Domain.Common;
using Domain.User.ValueObject;
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands;

[Authorize(Subscription = SubscriptionTypeEnum.Basic)]
public class CreateSubscriptionCommand : IRequest<ErrorOr<Subscription>>
{
    public Guid? UserId { get; set; }
    public SubscriptionType? SubscriptionType { get; set; }
}