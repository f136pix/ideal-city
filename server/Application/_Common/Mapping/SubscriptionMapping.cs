using Application.Subscriptions.Commands;
using Contracts.Subsriptions;
using Domain.Common;
using Mapster;

namespace Application._Common.Mapping;

public class SubscriptionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        
        // maps each property to the exactly corresponding
        // src -> dest
        config.NewConfig<CreateSubscriptionRequest, CreateSubscriptionCommand>()
            .Map(dest => dest.SubscriptionType, src => GetSubscriptionType(src.SubscriptionType.ToString()));
    }
    
    
    private SubscriptionType GetSubscriptionType(string subscriptionType)
    {
        return SubscriptionType.FromName(subscriptionType.ToString());
    }
}