using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.CompanyInfoResponses
{
    public class Organization
    {
        [JsonProperty("НаимПолнЮЛ")]
        public string FullName { get; set; }

        [JsonProperty("Адрес")]
        public Address Address { get; set; }
    }
}
