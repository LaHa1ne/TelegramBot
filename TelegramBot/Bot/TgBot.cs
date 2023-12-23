using Telegram.Bot;

namespace TelegramBot.Bot
{
    public class TgBot
    {
        private readonly IConfiguration _configuration;
        private TelegramBotClient _botClient;

        public TgBot(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<TelegramBotClient> GetBotClient()
        {
            if (_botClient != null)
            {
                return _botClient;
            }

            _botClient = new TelegramBotClient(_configuration["Token"]);

            var hook = $"{_configuration["Url"]}api/message/update";
            await _botClient.SetWebhookAsync(hook);

            return _botClient;
        }
    }
}
