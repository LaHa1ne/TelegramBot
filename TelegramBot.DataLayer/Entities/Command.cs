using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.Entities
{
    public class Command
    {
        [Key]
        public long ChatId { get; set; }
        public string CommandName { get; set; } = null!;
        public string? CommandParameters { get; set; } 
    }
}
