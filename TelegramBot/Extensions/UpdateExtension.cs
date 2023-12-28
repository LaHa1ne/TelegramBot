using Telegram.Bot.Types;

namespace TelegramBot.Extensions
{
    public static class UpdateExtension
    {
        public static (long chatId, string CommandName, string? CommandParameters) GetCommandOptions(this Update update)
        {
            var chatId = update.Message!.Chat.Id;
            string[] commandParts;
            string commandName;
            string commandParameters;
            if (string.IsNullOrEmpty(update.Message.Text))
            {
                commandName = "";
                commandParameters = "";
            }
            else
            {
                commandParts = update.Message.Text!.Split(' ', 2);
                commandName = commandParts[0];
                commandParameters = commandParts.Length > 1 ? commandParts[1].Replace(" ", "") : null;
            }

            return (chatId, commandName!, commandParameters);
        }
    }
}
