using Application._Common.Interfaces;
using Application._Common.Models;
using Domain.UserAggregate.Events;
using MediatR;

namespace Application.Users.Events;

public class UserCreatedHandler : INotificationHandler<UserCreated>
{
    private readonly IAsyncBus _asyncBus;

    public UserCreatedHandler(IAsyncBus asyncBus)
    {
        _asyncBus = asyncBus;
    }

    public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(notification.user.ToString());
        
       // Transform Domain Event in Publishable Message
       var userCreatedMessage = PublishableDomainEvent.Create(notification);
       
        // Publish to async bus
        await _asyncBus.Publish<PublishableDomainEvent>(userCreatedMessage, QueueNames.UserRegistration);

    }
}