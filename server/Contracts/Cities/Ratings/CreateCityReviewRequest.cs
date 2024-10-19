namespace Contracts.Cities.Ratings;

public record CreateCityReviewRequest
{
    public Guid CityId { get; set; }
    public int Rating { get; set; }
    public string Review { get; set; }
}