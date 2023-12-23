using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.CompanyInfoResponses
{
    public class ItemsFull
    {
        [JsonProperty("items")]
        public List<ItemFull> ItemsList { get; set; }
    }
}
