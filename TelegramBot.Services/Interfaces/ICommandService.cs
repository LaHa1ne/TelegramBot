using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.DataLayer.Entities;

namespace TelegramBot.Services.Interfaces
{
    public interface ICommandService
    {
        Task UpdateLastCommand(long chatId, string commandName, string commandParameters = "");
        Task<Command?> GetLastCommand(long chatId);
    }
}
