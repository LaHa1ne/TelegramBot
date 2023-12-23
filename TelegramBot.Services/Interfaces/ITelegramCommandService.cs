using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.DataLayer.Entities;

namespace TelegramBot.Services.Interfaces
{
    public interface ITelegramCommandService
    {
        Task<string> StartCommand(long chatId);
        Task<string> HelpCommand(long chatId);
        Task<string> HelloCommand(long chatId);
        Task<string> InnCommand(long chatId, string inns);
        Task<string> FullCommand(long chatId, string inn);
        Task<object> EgrulCommand(long chatId, string inn);
        Task<Command?> LastCommand(long chatId);
    }
}
