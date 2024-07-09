using System.Text.Json.Serialization;
using Domain.City.ValueObjects;
using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Countries;

namespace Domain.Cities;

public sealed class City : AggregateRoot<CityId>
{
    private readonly List<CityReview> _reviews = new();
    private readonly List<CityReviewId> _reviewsIds = new();


    public string Name { get; private set; }
    public CountryId CountryId { get; private set; }
    public Country Country { get; private set; }
    public Indicator? Indicators { get; private set; }
    public Weather? Weather { get; private set; }
    public AverageRating? AverageRating { get; private set; }
    public IReadOnlyList<CityReview> Reviews => _reviews.AsReadOnly();
    public IReadOnlyList<CityReviewId> ReviewsIds => _reviewsIds.AsReadOnly();


    private City(CityId cityId, string name, CountryId countryId, Indicator? indicators, Weather? weather,
        List<CityReview>? reviews) : base(cityId)
    {
        Name = name;
        CountryId = countryId;
        Indicators = indicators;
        Weather = weather;
        _reviews = reviews;
    }

    public static City Create(
        string name,
        CountryId countryId,
        Indicator? indicators,
        Weather? weather,
        List<CityReview>? reviews
    )
    {
        // add domain event
        return new City(CityId.CreateUnique(), name, countryId, indicators, weather, reviews);
    }

    // parameterless ctor for ef
#pragma warning disable CS8618
    private City()
    {
    }
#pragma warning restore CS8618
}