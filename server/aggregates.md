## Modelling

**Country:**

```json 
{
  "id": "0000-0000",
  "name": "Brazil",
  "citiesIds": [
    "0000-0001",
    "0000-0002"
  ]
}
```
**City:**

```json
{
  "id": "0000-0001",
  "name": "Rio de Janeiro",
  "countryId": "0000-0000",
  "indicators": {
    "population": 1000000,
    "area": 1000,
    "gdp": 1000000
  },
  "weather": {
    "temperature": 30,
    "humidity": 80,
    "precipitation": 0
  },
  "cityReviews": [
    {
      "userId": "0000-0005",
      "rating": 5,
      "comment": "Nice place to visit"
    },
    {
      "userId": "0000-0006",
      "rating": 4,
      "comment": "Nice place to visit"
    }
  ]
}
```

**User:**

```json
{
  "id": "0000-0005",
  "name": "John Doe",
  "email": "mail@.com",
  "cityReviewsIds": [
    "0000-0003",
    "0000-0004"
  ]
}
```
```
Country:
- CityId[]

City:
- Indicators // value objects
- Wheather 
- CityReviews[] 
    -UserId
    
User:
- CityReviewsId[] 
```

### Migration CLI   
`dotnet ef database update -p Infraestructure -s Api`
