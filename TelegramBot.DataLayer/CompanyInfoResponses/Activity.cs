using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.CompanyInfoResponses
{
    public class Activity
    {
        [JsonProperty("Код")]
        public string Code { get; set; }

        [JsonProperty("Текст")]
        public string Text { get; set; }
    }
}
