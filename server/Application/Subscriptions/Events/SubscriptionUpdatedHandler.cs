using Application._Common.Interfaces;
using Application._Common.Models;
using Domain.UserAggregate.Events;
using MediatR;

namespace Application.Subscriptions.Events;

public class SubscriptionUpdatedHandler : INotificationHandler<SubscriptionUpdated>
{
    private readonly IAsyncBus _asyncBus;

    public SubscriptionUpdatedHandler(IAsyncBus asyncBus)
    {
        _asyncBus = asyncBus;
    }

    public async Task Handle(SubscriptionUpdated notification, CancellationToken cancellationToken)
    {
        // Posts the created subscription to the async queue so its consumed in the payment service

        var subscriptionCreatedMessage = PublishableDomainEvent.Create(notification);

        await _asyncBus.PublishAsync<PublishableDomainEvent>(subscriptionCreatedMessage, QueueNames.SubscriptionRegistration);
    }
}