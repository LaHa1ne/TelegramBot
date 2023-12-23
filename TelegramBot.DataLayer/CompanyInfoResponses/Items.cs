using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.CompanyInfoResponses
{
    public class Items
    {
        [JsonProperty("items")]
        public List<Item> ItemsList { get; set; }
    }
}
