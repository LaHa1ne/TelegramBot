using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TelegramBot.DataLayer.Database;
using TelegramBot.DataLayer.Entities;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Services.Services
{
    public class CommandService : ICommandService
    {
        protected readonly ApplicationDbContext _db;
        public CommandService(ApplicationDbContext db) => _db = db;

        public async Task UpdateLastCommand(long chatId, string commandName, string commandParameters = "")
        {
            var command = await _db.Commands.FirstOrDefaultAsync(c => c.ChatId == chatId);
            if (command == null)
            {
                command = new Command()
                {
                    ChatId = chatId,
                    CommandName = commandName,
                    CommandParameters = commandParameters
                };
                await _db.Commands.AddAsync(command);
            }
            else
            {
                command.CommandName = commandName;
                command.CommandParameters = commandParameters;
                _db.Commands.Update(command);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<Command?> GetLastCommand(long chatId)
        {
            return await _db.Commands.FirstOrDefaultAsync(c => c.ChatId == chatId);
        }
    }
}