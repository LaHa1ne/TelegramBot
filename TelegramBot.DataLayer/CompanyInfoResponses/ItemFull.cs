using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.CompanyInfoResponses
{
    public class ItemFull
    {
        [JsonProperty("ЮЛ")]
        public OrganizationFull LE { get; set; }
    }
}
