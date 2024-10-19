using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Application._Common.Interfaces;
using Application._Common.Models;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Common.Async;

public class SqsBus : IAsyncBus
{
    private readonly IAmazonSQS _sqsClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private string? _queueUrl;

    public SqsBus(IAmazonSQS sqsClient, JsonSerializerOptions jsonOptions, IConfiguration configuration)
    {
        _sqsClient = sqsClient;
        _jsonOptions = jsonOptions;
        GetQueueUrl(configuration.GetSection("SqsSettings:QueueName").Value).Wait();
    } 

    public void Subscribe(string queueName)
    {
        // This service does not consume anything
        throw new NotImplementedException();
    }

    public async Task PublishAsync<T>(T @event, QueueNames queueName) where T : IPublishableMessage
    {
        var messageBody = JsonSerializer.Serialize(@event, _jsonOptions);
        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = _queueUrl,
            MessageBody = messageBody,
        };

        await _sqsClient.SendMessageAsync(sendMessageRequest);
    }
    
    private async Task<string> GetQueueUrl(string queueName)
    {
        var response = await _sqsClient.GetQueueUrlAsync(queueName);
        _queueUrl = response.QueueUrl;
        return response.QueueUrl;
    }
}