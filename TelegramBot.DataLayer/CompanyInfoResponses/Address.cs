using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.CompanyInfoResponses
{
    public class Address
    {
        [JsonProperty("АдресПолн")]
        public string FullAddress { get; set; }
    }
}
