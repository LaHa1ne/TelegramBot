using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Services.Interfaces
{
    public interface ICompanyInformationService
    {
        Task<HttpResponseMessage> GetCompanyInfoByInn(string inn);
        Task<HttpResponseMessage> GetPdfFileWithExtract(string inn);
    }
}
