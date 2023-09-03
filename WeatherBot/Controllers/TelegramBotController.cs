using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Model;
using WeatherBot.Model.Helper;
using WeatherBot.Model.Services;

namespace WeatherBot.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class TelegramBotController : ControllerBase
    {
        private readonly CommandExecutor commandExecutor;
        private readonly TelegramBotClient botClient;

        public TelegramBotController(CommandExecutor executor, TelegramBotClientManager telegramBot)
        {
            commandExecutor = executor;
            botClient = telegramBot.GetBotClientAsync().Result;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]object body)
        {
            var update = JsonConvert.DeserializeObject<Update>(body.ToString());
            if (update?.Message?.Text == null && update?.CallbackQuery == null && update?.Message?.Location == null)
            {
                return BadRequest();
            }

            await PrintSenderInfo(update);

            await commandExecutor.StartExecution(update);
            
            return Ok();
        }


        //todo: удалить перед публикацией.
        private async Task PrintSenderInfo(Update update)
        {
            Chat? chat = TelegramDataHelper.GetChat(update);
            string? message = TelegramDataHelper.GetCommandText(update);

            await Console.Out.WriteLineAsync("----------------------------");
            await Console.Out.WriteLineAsync($"{message} | {chat?.Username} | {DateTime.Now.ToShortTimeString()}");
            await Console.Out.WriteLineAsync("----------------------------");
        }
    }

}
