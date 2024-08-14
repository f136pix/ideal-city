using Contracts.Cities;
using Infraestructure.Common.Async.Requests;
using MediatR;

namespace Infraestructure.Common.Async.Handlers;

public class CreateCityQueueHandler : IHandler<CreateCityQueueRequest>
{
    ISender _mediator;

    public CreateCityQueueHandler(ISender mediator)
    {
        _mediator = mediator;
    }

    public void Handle(CreateCityQueueRequest request)
    {
        Console.WriteLine("Is here --> <---");
    }
}