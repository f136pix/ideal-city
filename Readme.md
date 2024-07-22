### Rich domain Model

Its usually easier to control the flow of your application having a rich domain model instad of a anemic one.s

### Keeping track of Ids

You can keep track of the many belonging Ids of a entity either by retrieving them the from the target entity when
requested:

```csharp
// City.cs
    private List<CityReviewId>? _reviewsIds => GetReviewsIds();
    public IReadOnlyList<CityReviewId>? ReviewsIds => _reviewsIds.AsReadOnly();

    // helper method
     private List<CityReviewId> GetReviewsIds()
    {
        if (Reviews == null) return null;
        // return Reviews.Select(r => r.Id).ToList();
        
        var reviewsIds =
            from review in Reviews
            select review.Id;

        return reviewsIds.ToList();
    }
```

This method does not require aditional configuration on the Ef Configuration.

````csharp
// CityConfiguration.cs
    builder.Ignore(c => c.ReviewsIds);
````

Or you can have a field/column that formats and saves them in the database in a single string.

```csharp
// City.cs
    private List<CityReviewId>? _reviewIds = new();
    public IReadOnlyList<CityReviewId>? ReviewsIds => _reviewsIds.AsReadOnly();
```

````csharp
// CityConfiguration.cs
    builder.Property(c => c.ReviewIds)
        .HasListOfIdsConverter();
        
    builder.Metadata.FindNavigation(nameof(City.ReviewIds))!
        .SetPropertyAccessMode(PropertyAccessMode.Field);
````

````csharp
// HasListOfIdsConverter.cs
    public static PropertyBuilder<List<Guid>> HasListOfIdsConverter(this PropertyBuilder<List<Guid>> propertyBuilder)
    {
        var converter = new ValueConverter<List<Guid>, string>(
        v => string.Join(";", v.Select(id => id.Value)),
        v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(value => CityReviewId.Create(Guid.Parse(value))).ToList());
        propertyBuilder.HasConversion(converter);

        return propertyBuilder;
    }
````

