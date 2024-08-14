using Application._Common.Models;
using ErrorOr;

namespace Application._Common.Interfaces;

public interface IAsyncBus
{
    public void Subscribe(string queueName);
    
    // public ErrorOr<Success> Publish<T>(T @event) where T : IPublishableMessage;
    public Task Publish<T>(T @event, QueueNames queueName) where T : IPublishableMessage;
}