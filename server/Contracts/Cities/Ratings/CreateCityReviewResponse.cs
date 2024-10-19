namespace Contracts.Cities.Ratings;

public record CreateCityReviewResponse(Guid CityId, int Rating, string Review);