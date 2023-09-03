using Telegram.Bot.Types;

namespace WeatherBot.Model.Command
{
    public interface ICommand
    {
        public string CommandName { get; }
        public Task Execute(Update update);
    }
}
