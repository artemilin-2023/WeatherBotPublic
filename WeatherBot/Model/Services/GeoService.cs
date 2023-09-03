using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Nodes;
using Telegram.Bot.Types;
using WeatherBot.Model.Entitys;
using WeatherBot.Model.Services.Interfaces;

namespace WeatherBot.Model.Services
{
    public class GeoService : IGeoService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        private readonly DataContext database;

        public GeoService(IConfiguration configuration, IServiceProvider serviceProvider, DataContext database)
        {
            this.configuration = configuration;
            httpClient = serviceProvider.GetRequiredService<HttpClientManager>().GetClient();
            this.database = database;
        }

        public async Task<City> GetCityAsync(long id)
        {
            return await database.Cities.FirstAsync(c => c.Id == id);
        }

        public async Task<City?> GetCityAsync(Location location)
        {
            var lat = NormalizeCoordinate(location.Latitude, 2);
            var lon = NormalizeCoordinate(location.Longitude, 2);

            var tryGetCity = await TryGetCityFromDatabase(lat, lon);
            if (tryGetCity.IsSuccess)
            {
                return tryGetCity.Result;
            }

            return await GetCityFromApi(lat, lon);
        }

        /// <summary>
        /// Нормализует координату до указанной точности после запятой, при этом не округляя значение.
        /// </summary>
        private double NormalizeCoordinate(double coordinate, int precision)
        {
            return (int)(coordinate * Math.Pow(10, precision)) / Math.Pow(10, precision);
        }

        private async Task<(bool IsSuccess, City? Result)> TryGetCityFromDatabase(double latitude, double longitude)
        {
            var city = await database.Cities.FirstOrDefaultAsync(c => c.Latitude == latitude && c.Longitude == longitude);

            return (city != null, city);
        }

        private async Task<City?> GetCityFromApi(double latitude, double longitude)
        {
            var key = configuration["WeatherApiToken"];
            var queryString = $"timezone.json?key={key}&q={latitude.ToString().Replace(",", ".")},{longitude.ToString().Replace(",", ".")}";

            using (var response = await httpClient.GetAsync(queryString))
            {
                if (response.IsSuccessStatusCode)
                {
                    var city = JsonSerializer.Deserialize<UserLocation>(await response.Content.ReadAsStringAsync())!.City;
                    await SaveCityToDatabase(city);

                    return city;
                }

                throw new Exception($"Не удалось получить город. Сервер ответил с ошибкой: {response.StatusCode}");
            }
        }

        private async Task SaveCityToDatabase(City city)
        {
            await database.Cities.AddAsync(city);
            await database.SaveChangesAsync();
        }

        public async Task<string?> GetTimeZoneAsync(City city)
        {
            var key = configuration["WeatherApiToken"];
            var queryString = $"timezone.json?key={key}&q={city.Latitude.ToString().Replace(",", ".")},{city.Longitude.ToString().Replace(",", ".")}";

            using (var response = await httpClient.GetAsync(queryString))
            {
                if (response.IsSuccessStatusCode)
                {
                    return ExtractingTimeZone(await response.Content.ReadAsStringAsync());
                }

                throw new Exception($"Не удалось получть временную зону. Сервер ответил с ошибкой: {response.StatusCode}");
            }
        }

        private string ExtractingTimeZone(string content)
        {
            var data = JsonNode.Parse(content); 
            return (string)data!["location"]!["tz_id"]!;
        }
    }
}
