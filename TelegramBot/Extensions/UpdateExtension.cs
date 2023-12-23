using Telegram.Bot.Types;

namespace TelegramBot.Extensions
{
    public static class UpdateExtension
    {
        public static (long chatId, string CommandName, string? CommandParameters) GetCommandOptions(this Update update)
        {
            var chatId = update.Message!.Chat.Id;

            var commandParts = update.Message.Text!.Split(' ', 2);
            var commandName = commandParts[0];
            var commandParameters = commandParts.Length > 1 ? commandParts[1].Replace(" ", "") : null;

            return (chatId, commandName!, commandParameters);
        }
    }
}
