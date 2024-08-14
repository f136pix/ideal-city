namespace Application._Common.Interfaces;

public interface IPublishableMessage
{
    Guid Id { get; }
    string EventType { get; }
    object Data { get; }
}