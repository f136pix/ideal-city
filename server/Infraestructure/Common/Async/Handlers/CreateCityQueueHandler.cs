using Contracts.Cities;
using Infraestructure.Common.Async.Requests;
using MediatR;

namespace Infraestructure.Common.Async.Handlers;

public class CreateCityQueueHandler : IHandler<CreateCityQueueRequest>
{
    public async Task Handle(CreateCityQueueRequest request)
    {
        Console.WriteLine("Is here --> <---");
    }
}