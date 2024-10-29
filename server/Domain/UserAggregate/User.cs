using System.Collections.ObjectModel;
using Domain._Common.Interfaces;
using Domain.City.ValueObjects;
using Domain.Common;
using Domain.User.Entities;
using Domain.User.ValueObject;
using Domain.UserAggregate.Events;
using ErrorOr;

namespace Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    private readonly List<Post> _posts = new();
    private List<PostId>? _postIds => GetPostsIds();
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Subscription Subscription { get; private set; }
    public SubscriptionId SubscriptionId { get; private set; }
    public CityAggregate.City? City { get; private set; }
    public CityId? CityId { get; private set; }
    public string Password { get; private set; }
    public string? ProfilePicture { get; private set; }
    public string? Bio { get; private set; }
    public IReadOnlyList<Post> Posts { get; private set; } // Using property access style
    public IReadOnlyList<PostId>? PostIds => _postIds.AsReadOnly();


    private User(UserId userId, string name, string email, Subscription subscription, CityAggregate.City? city,
        string password,
        string? profilePicture, string? bio) : base(userId)
    {
        Name = name;
        Email = email;
        // Subscription = subscription
        SubscriptionId = subscription.Id;
        CityId ??= city?.Id;
        Password = password;
        ProfilePicture ??= profilePicture;
        Bio ??= bio;
    }

    public static User Create(
        string name,
        string email,
        Subscription subscription,
        CityAggregate.City? city,
        string password,
        string? profilePicture,
        string? bio
    )
    {
        var user = new User(UserId.CreateUnique(), name, email, subscription, city, password, profilePicture,
            bio);

        // add domain event
        user.AddDomainEvent(new UserCreated(email, name, subscription.SubscriptionType, subscription.Id.Value));

        return user;
    }

    public ErrorOr<Updated> Update(
        string? name,
        string? email,
        CityAggregate.City? city,
        string? password,
        string? profilePicture,
        string? bio
    )
    {
        Name ??= Name;
        Email ??= Email;
        if (city is not null)
        {
            City = city;
            CityId = CityId;
        }

        Password ??= Password;
        ProfilePicture ??= ProfilePicture;
        Bio ??= Bio;

        return Result.Updated;
    }

    public ErrorOr<Updated> UpdateSubscription(Subscription subscription)
    {
        if (subscription.SubscriptionType.Value == SubscriptionType.Basic.Value)
        {
            return Error.Conflict("Can't update to Free subscription type");
        }

        Subscription = subscription;
        SubscriptionId = Subscription.Id;

        var result = subscription.AddUser(this);
        if (result.IsError) return result.Errors;
        
        subscription.AddDomainEvent(new SubscriptionUpdated(subscription));

        return Result.Updated;
    }

    public ErrorOr<Updated> LeaveSubscription()
    {
        var result = Subscription.RemoveUser(this);
        if (result.IsError) return result.Errors;
        
        ErrorOr<Subscription> newSubscription = Subscription.Create(SubscriptionType.Basic);
        Subscription = newSubscription.Value;
        SubscriptionId = newSubscription.Value.Id;
        
        newSubscription.Value.AddDomainEvent(new SubscriptionUpdated(newSubscription.Value));

        return Result.Updated;
    }

    private List<PostId>? GetPostsIds()
    {
        if (Posts == null) return new List<PostId>();

        // return Posts.Select(p => p.Id).ToList();

        var postIds =
            from post in Posts
            select post.Id;

        return postIds.ToList();
    }
    
    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, Password);
    }
    
#pragma warning disable CS8618

    public User()
    {
    }
#pragma warning restore CS8618
}