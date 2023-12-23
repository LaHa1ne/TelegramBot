using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Services.Services
{
    public class CompanyInformationService : ICompanyInformationService
    {
        protected readonly IConfiguration _configuration;
        protected readonly IHttpClientFactory _httpClientFactory;
        public CompanyInformationService(IConfiguration configuration, IHttpClientFactory httpClientFactory) 
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetCompanyInfoByInn(string inn)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var url = $"{_configuration["CompanyInfoApi"]}?req={inn}&key={_configuration["PersonalKeyForGetCompanyInfo"]}";
                var response = await httpClient.GetAsync(url);
                return response;
            }
        }

        public async Task<HttpResponseMessage> GetPdfFileWithExtract(string inn)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var url = $"{_configuration["PdfFileWithExtractApi"]}?req={inn}&key={_configuration["PersonalKeyForGetCompanyInfo"]}";
                var response = await httpClient.GetAsync(url);
                return response;
            }
        }


    }
}
