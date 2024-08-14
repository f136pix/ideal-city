using Infraestructure.Common.Async.Requests;

namespace Infraestructure.Common.Async;

public class AsyncEventsHandlerDictionary : Dictionary<string, Type>
{
    // Relates a routing key with its contract request 
    public AsyncEventsHandlerDictionary()
    {
        this.Add("city.created", typeof(CreateCityQueueRequest));

    }
}

