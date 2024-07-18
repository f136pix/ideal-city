using System.Collections.ObjectModel;
using Domain.City.ValueObjects;
using Domain.Common;
using Domain.User.Entities;
using Domain.User.ValueObject;

namespace Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    private readonly List<Post> _posts = new();
    private List<PostId> _postIds => GetPostsIds();
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Subscription Subscription { get; private set; }
    public SubscriptionId SubscriptionId { get; private set; }
    public CityAggregate.City? City { get; private set; }
    public CityId CityId { get; private set; }
    public string Password { get; private set; }
    public string? ProfilePicture { get; private set; }
    public string? Bio { get; private set; }
    public IReadOnlyList<Post> Posts => _posts.AsReadOnly();
    public IReadOnlyList<PostId> PostIds => _postIds.AsReadOnly();


    private User(UserId userId, string name, string email, CityAggregate.City city, CityId cityId, string password,
        string? profilePicture, string? bio) : base(userId)
    {
        Name = name;
        Email = email;
        // Subscription = Subscription.Create();
        City = city;
        CityId = cityId;
        Password = password;
        ProfilePicture = profilePicture;
        Bio = bio;
    }

    public static User Create(
        string name,
        string email,
        CityAggregate.City city,
        CityId cityId,
        string password,
        string? profilePicture,
        string? bio
    )
    {
        var user = new User(UserId.CreateUnique(), name, email, city, cityId, password, profilePicture, bio);

        // add domain event
        // user.AddDomainEvent(new UserCreated(user));

        return user;
    }

    private List<PostId> GetPostsIds()
    {
        // return Posts.Select(p => p.Id).ToList();

        var postIds =
            from post in Posts
            select post.Id;

        return postIds.ToList();
    }

#pragma warning disable CS8618
    public User()
    {
    }
#pragma warning restore CS8618
}