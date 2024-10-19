using Domain.Cities;
using Domain.Cities.Events;
using Domain.City.ValueObjects;
using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Countries;
using Domain.CountryAggregate;
using Mapster;

namespace Domain.CityAggregate;

public sealed class City : AggregateRoot<CityId>
{
    private readonly List<CityReview> _reviews = new();
    private List<CityReviewId>? _reviewsIds => GetReviewsIds();
    private readonly List<UserAggregate.User> _users = new();
    private List<UserId> _usersIds => GetUsersIds();
    // private readonly List<UserAggregate.User> _favouritedByUsers = new();

    public string Name { get; private set; }
    public CountryId CountryId { get; private set; }
    public Country Country { get; private set; }
    public Indicator? Indicators { get; private set; }
    public Weather? Weather { get; private set; }
    public AverageRating? AverageRating { get; private set; }
    public IReadOnlyList<CityReview>? Reviews => _reviews.AsReadOnly();
    public IReadOnlyList<CityReviewId>? ReviewsIds => _reviewsIds.AsReadOnly();
    public IReadOnlyList<UserAggregate.User>? Users => _users.AsReadOnly();
    public IReadOnlyList<UserId> UsersIds => _usersIds.AsReadOnly();
    // public IReadOnlyList<UserAggregate.User>? FavouritedByUsers => _favouritedByUsers.AsReadOnly();


    private City(CityId cityId, string name, CountryId countryId, Country country, Indicator? indicators,
        Weather? weather,
        List<CityReview>? reviews) : base(cityId)
    {
        Name = name;
        CountryId = countryId;
        Country = country;
        Indicators = indicators;
        Weather = weather;
        _reviews = reviews;
    }

    public static City Create(
        string name,
        Country country,
        Indicator? indicators,
        Weather? weather,
        List<CityReview>? reviews
    )
    {
        var city = new City(CityId.CreateUnique(), name, country.Id, country, indicators, weather, reviews);

        // add domain event
        city.AddDomainEvent(new CityCreated(city));

        return city;
    }

    public void SetCountry(CountryId countryId) // sets the country of a city
    {
        CountryId = countryId;
    }

    public void AddReview(CityReview cityReview)
    {
        _reviews.Add(cityReview);
    }

    private List<CityReviewId> GetReviewsIds()
    {
        var reviewsIds =
            from review in Reviews
            select review.Id;

        return reviewsIds.ToList();
    }

    private List<UserId> GetUsersIds()
    {
        var userIds =
            from user in Users
            select user.Id;

        return userIds.ToList();
    }

    // parameterless ctor for ef
#pragma warning disable CS8618
    public City()
    {
    }
#pragma warning restore CS8618
}