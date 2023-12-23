using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.DataLayer.CompanyInfoResponses;
using TelegramBot.DataLayer.DTOs;

namespace TelegramBot.DataLayer.Extensions
{
    public static class ItemsExtension
    {
        public static CompanyInfoDTO? ToCompanyInfoDTO(this Items items, string inn)
        {
            if (items?.ItemsList?.Count > 0)
            {
                var organization = items.ItemsList[0].LE;
                if (organization != null)
                {
                    return new CompanyInfoDTO()
                    {
                        INN = inn,
                        Name = organization.FullName,
                        Address = organization.Address == null ? "" : organization.Address.FullAddress,
                        ResponseDescription = "Информация о компании получена"
                    };
                }
            }
            return null;
        }
    }
}
