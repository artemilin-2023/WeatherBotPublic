using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherBot.Model.Entitys;
using WeatherBot.Model.Services.Interfaces;

namespace WeatherBot.Model.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public WeatherService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            httpClient = serviceProvider.GetRequiredService<HttpClientManager>().GetClient();
            this.configuration = configuration;
        }

        public async Task<Weather?> GetCurrentWeatherAsync(City city)
        {
            var key = configuration["WeatherApiToken"];
            var queryString = $"current.json?key={key}&q={city.Latitude.ToString().Replace(",", ".")},{city.Longitude.ToString().Replace(",", ".")}&lang=ru";

            using (var response = await httpClient.GetAsync(queryString))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await JsonSerializer.DeserializeAsync<Weather>(await response.Content.ReadAsStreamAsync());
                }

                throw new Exception($"Не удалось получить погоду. Сервер ответил с кодом: {response.StatusCode}");
            }
        }

        public async Task<Weather> GetForecastWeather(City city, int daysForecast)
        {
            throw new NotImplementedException();
        }
    }
}
