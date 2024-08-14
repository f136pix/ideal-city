using Infraestructure.Common.Async.Requests;

namespace Infraestructure.Common.Async.Handlers;

public interface IHandler<T> where T: IQueueRequest
{
    Task Handle(T request);
}