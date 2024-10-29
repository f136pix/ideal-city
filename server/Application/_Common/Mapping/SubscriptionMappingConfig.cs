using Application.Subscriptions.Commands;
using Application.Subscriptions.Commands.RemoveUserFromSubscription;
using Contracts.Subscriptions;
using Contracts.Subscriptions.RemoveUserFromSubscription;
using Contracts.Subsriptions;
using Domain.User.ValueObject;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Update.Internal;
using SubscriptionType = Domain.Common.SubscriptionType;

namespace Application._Common.Mapping;

public class SubscriptionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        
        // maps each property to the exactly corresponding
        // src -> dest
        config.NewConfig<CreateSubscriptionRequest, CreateSubscriptionCommand>()
            .Map(dest => dest.SubscriptionType, src => GetSubscriptionType(src.SubscriptionType.ToString()));

        config.NewConfig<Subscription, CreateSubscriptionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.SubscriptionType, src => src.SubscriptionType.Name)
            .Map(dest => dest.IsActive, src => src.IsActive);

        config.NewConfig<Subscription, RemoveUserFromSubscriptionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.SubscriptionType, src => src.SubscriptionType.Name);
    }
    
    private SubscriptionType GetSubscriptionType(string subscriptionType)
    {
        return SubscriptionType.FromName(subscriptionType.ToString());
    }
    
}