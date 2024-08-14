using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application._Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infraestructure.Common.Async;

public sealed class RabbitMqBus : IEventBus
{
    // private readonly ILogger<RabbitMqBus> _logger;
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _evenTypes;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string _hostName;

    public RabbitMqBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory, string hostName)
        // ILogger<RabbitMqBus> logger)
    {
        _mediator = mediator;
        _serviceScopeFactory = serviceScopeFactory;
        _handlers = new Dictionary<string, List<Type>>();
        _evenTypes = new List<Type>();
        _hostName = hostName;
        // _logger = logger;
    }

    public Task SendCommand<T>(T command) where T : IRequest
    {
        return _mediator.Send(command);
    }

    public async void Publish<T>(T @event) where T : Event
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName
        };
        using IConnection connection = await factory.CreateConnectionAsync();
        using IChannel channel = await connection.CreateChannelAsync();
        var eventName = @event.GetType().Name;
        await channel.QueueDeclareAsync(eventName, true, false, false, passive: true);
        var message =  JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync("", eventName, null, body);
    }

    public void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(TH);

        if (!_evenTypes.Contains(typeof(T)))
        {
            _evenTypes.Add(typeof(T));
        }

        if (!_handlers.ContainsKey(eventName))
        {
            _handlers.Add(eventName, new List<Type>());
        }

        if (_handlers[eventName].Any(s => s == handlerType))
        {
            _logger.LogError($"Handler Type {handlerType.Name} already is registered for '{eventName}'",
                nameof(handlerType));
            throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'",
                nameof(handlerType));
        }

        _handlers[eventName].Add(handlerType);

        StartBasicConsumer<T>();
    }

    private async void StartBasicConsumer<T>() where T : Event
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
            DispatchConsumersAsync = true
        };

        IConnection connection = await factory.CreateConnectionAsync();
        IChannel channel = await connection.CreateChannelAsync();

        var eventName = typeof(T).Name;
        await channel.QueueDeclareAsync(eventName, false, false, false, null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += Consumer_Received;

        await channel.BasicConsumeAsync(eventName, false, consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        var eventName = e.RoutingKey;
        var message = Encoding.UTF8.GetString(e.Body.Span);

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
            ((AsyncDefaultBasicConsumer)sender).Model.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Err : " + exception.Message);
            // _logger.LogError(exception, "Something went wrong with Consumer_Received!");
        }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (_handlers.ContainsKey(eventName))
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var subscriptions = _handlers[eventName];
                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ServiceProvider.GetService(subscription);

                    if (handler == null)
                    {
                        continue;
                    }

                    var eventType = _evenTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonSerializer.Deserialize(message, eventType);
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}