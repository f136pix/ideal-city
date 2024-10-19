using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application._Common.Interfaces;
using Application._Common.Models;
using Infraestructure.Common.Async.Handlers;
using Infraestructure.Common.Async.Requests;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infraestructure.Common.Async;

public sealed class RabbitMqBus : IAsyncBus
{
    // private readonly ILogger<RabbitMqBus> _logger;
    private readonly IMediator _mediator;
    private readonly AsyncEventsHandlerDictionary _handlers = new AsyncEventsHandlerDictionary();
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string _hostName;
    private readonly IMapper _mapper;

    public RabbitMqBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory, string hostName, IMapper mapper)
        // ILogger<RabbitMqBus> logger)
    {
        _mediator = mediator;
        _serviceScopeFactory = serviceScopeFactory;
        _hostName = hostName;
        _mapper = mapper;
        // _logger = logger;
    }

    // public Task SendCommand(IRequest command)
    // {
    //     return _mediator.Send(command);
    // }

    public async Task PublishAsync<T>(T @event, QueueNames queueName) where T : IPublishableMessage
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName
        };
        using IConnection connection = await factory.CreateConnectionAsync();
        using IChannel channel = await connection.CreateChannelAsync();

        var eventName = @event.EventType;
        await channel.QueueDeclareAsync(queueName.ToString(), true, false, false, passive: false);
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        var message = JsonSerializer.Serialize(@event, options);
        // var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);
        var properties = new BasicProperties();
        await channel.BasicPublishAsync("", eventName, properties, body, false);
    }

    public void Subscribe(string queue)
    {
        StartBasicConsumer(queue);
    }

    private async void StartBasicConsumer(string queueName)
    {
        Console.WriteLine("Starting consumer");
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
            DispatchConsumersAsync = true
        };

        IConnection connection = await factory.CreateConnectionAsync();
        IChannel channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queueName, false, false, false, null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += Consumer_Received;

        await channel.BasicConsumeAsync(queueName, true, consumer);

        // No auto Ack
        // var consumer = new AsyncEventingBasicConsumer(channel);
        // consumer.Received += async (model, ea) =>
        // {
        //     if (await Consumer_Received(model, ea))
        //     {
        //         channel.BasicAckAsync(ea.DeliveryTag, false);
        //     }
        // };
        // await channel.BasicConsumeAsync(queueName, false, consumer);

        await Task.Delay(TimeSpan.FromMinutes(1));
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        var eventName = e.RoutingKey;
        var message = Encoding.UTF8.GetString(e.Body.Span);

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Err : " + exception.Message);
        }
    }

    private async Task ProcessEvent(string routingKey, string message)
    {
        if (_handlers.ContainsKey(routingKey))
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                Type? contractType = _handlers[routingKey];
                // Type? contractType = Type.GetType($"Infraestructure.Common.Async.Requests.${routingKey}");
                dynamic queueRequest = JsonSerializer.Deserialize(message, contractType)!;

                // Gets the Handler for the incoming type
                Type handlerType = typeof(IHandler<>).MakeGenericType(contractType);
                dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);

                await handler.Handle((CreateCityQueueRequest)queueRequest);
            }
        }
    }
}