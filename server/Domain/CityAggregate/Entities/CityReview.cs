using Domain.Common;

namespace Domain.City.ValueObjects;

public sealed class CityReview : Entity<CityReviewId>
{
    public string Review { get; private set; }
    public int Rating { get; private set; }
    public CityId CityId { get; private set; }  
    public CityAggregate.City City { get; private set; }

    // ownerID

    private CityReview(CityReviewId cityReviewId, string review, int rating) : base(cityReviewId)
    {
        Review = review;
        Rating = rating;
    }

    public static CityReview Create(string review, int rating)
    {
        return new CityReview(CityReviewId.CreateUnique(), review, rating);
    }

#pragma warning disable CS8618
    private CityReview()
    {
    }
#pragma warning restore CS8618
}