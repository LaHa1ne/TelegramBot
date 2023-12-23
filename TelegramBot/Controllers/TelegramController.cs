using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot;
using TelegramBot.DataLayer.Commands;
using TelegramBot.Extensions;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class TelegramController : ControllerBase
    {
        protected readonly ITelegramBotClient _telegramBotClient;
        protected readonly ITelegramCommandService _telegramCommandService;

        public TelegramController(TgBot telegramBot, ITelegramCommandService telegramCommandService)
        {
            _telegramBotClient = telegramBot.GetBotClient().Result;
            _telegramCommandService = telegramCommandService;
        }

        [HttpPost]
        public async Task<IActionResult> Update(Update update)
        {
            if (update == null || update.Message == null)
            {
                return BadRequest();
            }

            (long chatId, string commandName, string? commandParameters) = update.GetCommandOptions();
            await ExecuteCommand(chatId, commandName, commandParameters);

            return Ok();
        }

        [HttpGet]
        public string Get()
        {
            return "Telegram bot was started";
        }

        protected async Task ExecuteCommand(long chatId, string commandName, string? commandParameters)
        {
            switch (commandName)
            {
                case CommandNames.StartCommand:
                    await _telegramBotClient.SendTextMessageAsync(chatId, await _telegramCommandService.StartCommand(chatId));
                    break;

                case CommandNames.HelpCommand:
                    await _telegramBotClient.SendTextMessageAsync(chatId, await _telegramCommandService.HelpCommand(chatId));
                    break;

                case CommandNames.HelloCommand:
                    await _telegramBotClient.SendTextMessageAsync(chatId, await _telegramCommandService.HelloCommand(chatId));
                    break;

                case CommandNames.InnCommand:
                    await _telegramBotClient.SendTextMessageAsync(chatId, await _telegramCommandService.InnCommand(chatId, commandParameters!));
                    break;

                case CommandNames.FullCommand:
                    await _telegramBotClient.SendTextMessageAsync(chatId, await _telegramCommandService.FullCommand(chatId, commandParameters!));
                    break;

                case CommandNames.EgrulCommand:
                    var response = await _telegramCommandService.EgrulCommand(chatId, commandParameters!);
                    if (response is string stringResponse)
                    {
                        await _telegramBotClient.SendTextMessageAsync(chatId, stringResponse);
                    }
                    else if (response is byte[] bytesResponse)
                    {
                        await _telegramBotClient.SendDocumentAsync(chatId, new InputFileStream(new MemoryStream(bytesResponse), $"{commandParameters}.pdf"));
                    }
                    else
                    {
                        await _telegramBotClient.SendTextMessageAsync(chatId, "Неизвестная ошибка");
                    }
                    break;

                case CommandNames.LastCommand:
                    var lastCommandOptions = await _telegramCommandService.LastCommand(chatId);
                    if (lastCommandOptions == null) await _telegramBotClient.SendTextMessageAsync(chatId, "Ни одной команды еще не было выполнено");
                    else await ExecuteCommand(lastCommandOptions.ChatId, lastCommandOptions.CommandName, lastCommandOptions.CommandParameters);
                    break;

                default:
                    await _telegramBotClient.SendTextMessageAsync(chatId, "Неизвестная команда");
                    break;
            }
        }
    }
}
