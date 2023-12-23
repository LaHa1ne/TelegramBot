using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.DTOs
{
    public class CompanyFullInfoDTO
    {
        public string INN { get; set; } = null!;
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string ResponseDescription { get; set; } = null!;
    }
}
