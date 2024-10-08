using Domain.Common;
using Domain.User.ValueObject;

namespace Domain.User.Entities;

public class Post : Entity<PostId>
{
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public string? Image { get; private set; }
    public UserId? UserId { get; private set; }
    public UserAggregate.User? User { get; private set; }

#pragma warning disable CS8618

    public Post()
    {
    }
#pragma warning restore CS8618
}