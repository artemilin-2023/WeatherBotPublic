using System.Linq.Expressions;
using Telegram.Bot;

namespace WeatherBot.Model
{
    public class TelegramBotClientManager
    {
        private readonly IConfiguration configuration;
        private TelegramBotClient botClient;

        public TelegramBotClientManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }

            botClient = new TelegramBotClient(configuration["TelegramToken"]);
            await botClient.SetWebhookAsync($"{configuration["url"]}api/message/update");

            return botClient;
        }
    }
}
