using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.DataLayer.CompanyInfoResponses
{
    public class OrganizationFull
    {
        [JsonProperty("НаимПолнЮЛ")]
        public string FullName { get; set; }

        [JsonProperty("Адрес")]
        public Address Address { get; set; }

        [JsonProperty("ОснВидДеят")]
        public Activity MainActivity { get; set; }

        [JsonProperty("ДопВидДеят")]
        public List<Activity> AdditionalActivities { get; set; }
    }
}
