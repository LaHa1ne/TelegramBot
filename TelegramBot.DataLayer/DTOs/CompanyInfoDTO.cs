using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.DTOs
{
    public class CompanyInfoDTO
    {
        [JsonProperty("ИНН")]
        public string INN { get; set; } = null!;

        [JsonProperty("НаимПолнЮЛ")]
        public string? Name { get; set; }

        [JsonProperty("Адрес")]
        public string? Address { get; set; }

        [JsonProperty("Статус")]
        public string ResponseDescription { get; set; } = null!;
    }
}
