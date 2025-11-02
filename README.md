# ApiAggregator

ApiAggregator is an ASP.NET Core Web API project that aggregates data from multiple external weather APIs. It provides endpoints to fetch weather information in a consolidated format. The project is designed with a service-oriented architecture for modularity and easy extension.

## Features

- Aggregates multiple external APIs into a single API endpoint.
- Modular architecture with Controllers, Services, and Models.
- Dependency Injection for services.
- Error handling and logging support.
- Easily extendable for adding new data sources.

## Project Structure

```
ApiAggregator/
│
├─ Controllers/       # API controllers
├─ Models/            # Data models
├─ Services/          # External API service logic
├─ Properties/        # Project properties
├─ Program.cs         # Main entry point
├─ ApiAggregator.csproj
└─ appsettings.json   # Configuration file for API keys and endpoints
```

### Controllers

Controllers to manually operate the API endpoints:

- `OpenWeatherController` — Controller to use the [OpenWeather](https://openweathermap.org/current) service as stand alone.
- `OpenMeteoController` — Controller to use the [OpenMeteo](https://open-meteo.com/en/docs) service as stand alone.
- `TomorrowController` — Controller to use the [Tomorrow](https://www.tomorrow.io/) service as stand alone.
- `LocationController` — Controller to use the [Location](https://openweathermap.org/api/geocoding-api) service as stand alone.
- `WeatherAggController` — Combines data from multiple services and returns the aggregated response.

### Services

Services contains the interfaces of each service as well as the logic behind the API calls:

- `ILocationService` — The Interface associated with the Location service.
- `IOpenMeteoService` — The Interface associated with the OpenMeteo service.
- `IOpenWeatherService` — The Interface associated with the OpenWeather service.
- `ITomorrowService` — The Interface associated with the Tomorrow service.
- `LocationService` — Handles calls to the Location API.
- `OpenMeteoService` — Handles calls to the OpenMeteo API.
- `OpenWeatherService` — Handles calls to the OpenWeather API.
- `TomorrowService` — Handles calls to the Tomorrow API.

### Models

Models define the data structures and methods used by their corresponding services and controllers.

- `OpenMeteo` — Contains methods and structures associated with OpenMeteo.
- `OpenWeather` — Contains methods and structures associated with OpenWeather.
- `Tomorrow` — Contains methods and structures associated with Tomorrow.

---

## How the Aggregator Works

The `WeatherAggController` provides a single endpoint that combines multiple services into a unified response.  

**Flow:**

1. The controller receives a request with required parameters (the name of the location, for which to pull weather data).
2. Internally it calls each registered service mentioned above.
3. Each service fetches data from its external API asynchronously.
4. Once all the calls have returned their data (or the error handling has finished in case something went wrong), The `WeatherAggController` then combines all the results into a single JSON object.
5. The controller responds with the unified JSON payload containing data from all services.

---

## Configuration

General application settings, API uris, keys and application parameters are configured in `appsettings.json`.

