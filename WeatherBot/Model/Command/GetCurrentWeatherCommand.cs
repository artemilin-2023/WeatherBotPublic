using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Model.Entitys;
using WeatherBot.Model.Helper;
using WeatherBot.Model.Services.Interfaces;

namespace WeatherBot.Model.Command
{
    public class GetCurrentWeatherCommand : ICommand
    {
        public string CommandName => CommandNames.Weather;

        private readonly IUserService userService;
        private readonly IWeatherService weatherService;
        private readonly IGeoService geoService;
        private readonly TelegramBotClient botClient;

        public GetCurrentWeatherCommand(IServiceProvider serviceProvider)
        {
            userService = serviceProvider.GetRequiredService<IUserService>();
            weatherService = serviceProvider.GetRequiredService<IWeatherService>();
            geoService = serviceProvider.GetRequiredService<IGeoService>();
            botClient = serviceProvider.GetRequiredService<TelegramBotClientManager>().GetBotClientAsync().Result;
        }

        public async Task Execute(Update update)
        {
            var chat = TelegramDataHelper.GetChat(update);

            var user = await userService.GetUserAsync(chat.Id);
            if (user == null)
            {
                await botClient.SendTextMessageAsync(chat.Id, Messages.ConfirmRegistratingMassage, replyMarkup: InlineButtons.ConfirmRegistrutingButtons);
                return;
            }

            var city = await geoService.GetCityAsync(user.CityId);
            var weather = await weatherService.GetCurrentWeatherAsync(city);

            await botClient.SendTextMessageAsync(chat.Id, BuildWeatherView(city.Name, weather!.Current));
        }

        private string BuildWeatherView(string cityName, Current current)
        {
            return $"Краткая сводка по городу: {cityName}\n\n" +
                   $"{current.Condition.Discription}\n" +
                   $"{current.TemperatureCelsius} °C (ощущается как {current.FeelsLikeCelsius} °C)\n" +
                   $"{current.WindSpeed} м/с, {ConvertWindDirection(current.WindDirection)}\n\n" +
                   $"Подробнее:\n\n" +
                   $"Давление: {ConvertMillibars(current.Pressure)} мм рт. ст.\n" +
                   $"Осадки: {current.Humidity} мм\n" +
                   $"Облачность: {current.Cloud}\n" +
                   $"Индекс УФ-излучения: {current.UvIndex}\n\n" +
                   $"Последнее обновление было {DateTime.Parse(current.LastUpdate).ToShortDateString()} в {DateTime.Parse(current.LastUpdate).ToShortTimeString()}";
        }

        /// <summary>
        /// Переводит давление из миллибаров в миллиметры ртутного столба.
        /// </summary>
        private double ConvertMillibars(double millibars) => Math.Round(millibars / 1000 * 750.063783, 2);

        private string ConvertWindDirection(string windDirection)
        {
            return windDirection;
        }
    }
}
