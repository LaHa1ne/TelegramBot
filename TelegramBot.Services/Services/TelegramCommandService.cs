using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TelegramBot.DataLayer.Commands;
using TelegramBot.DataLayer.CompanyInfoResponses;
using TelegramBot.DataLayer.DTOs;
using TelegramBot.DataLayer.Entities;
using TelegramBot.DataLayer.Extensions;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Services.Services
{
    public class TelegramCommandService : ITelegramCommandService
    {
        protected readonly ICompanyInformationService _companyInformationService;
        protected readonly ICommandService _commandService;
        public TelegramCommandService(ICompanyInformationService companyInformationService, ICommandService commandService) 
        {
            _companyInformationService = companyInformationService;
            _commandService = commandService;
        }
        public async Task<string> StartCommand(long chatId)
        {
            await _commandService.UpdateLastCommand(chatId, CommandNames.StartCommand);
            var res_str = "Привет. Для уточнения списка доступных команд введите /help";
            return res_str;
        }

        public async Task<string> HelpCommand(long chatId)
        {
            await _commandService.UpdateLastCommand(chatId, CommandNames.HelpCommand);
            var res_str = "Список команд:\n" +
                $"{CommandNames.StartCommand} - начать общение с ботом\n" +
                $"{CommandNames.HelpCommand} - получить список всех доступных команд\n" +
                $"{CommandNames.InnCommand} ИНН_1[,ИНН_2,...,ИНН_n] - получить информацию о названиях и адресах компаний с указанными ИНН\n" +
                $"{CommandNames.FullCommand} ИНН - получить подробную информацию о комании, включая ее основные и доп. виды деятельности\n" +
                $"{CommandNames.EgrulCommand} ИНН - получить pdf-файл с выпиской из ЕГРЮЛ\n" +
                $"{CommandNames.LastCommand} - повторить последнюю успешно выполненную команду\n";
            return res_str;

        }

        public async Task<string> HelloCommand(long chatId)
        {
            await _commandService.UpdateLastCommand(chatId, CommandNames.HelloCommand);
            var res_str = "Лосев Артём, laa-1028@mail.ru, https://github.com/LaHa1ne/TelegramBot";
            return res_str;
        }

        public async Task<string> InnCommand(long chatId, string inns)
        {
            try
            {
                if (string.IsNullOrEmpty(inns)) return "Некорректные параметры";
                var innList = inns.Split(',').ToList();
                var companyInfoList = new List<CompanyInfoDTO>(innList.Count);
                foreach (var inn in innList)
                {
                    CompanyInfoDTO companyInfo = null;
                    var response = await _companyInformationService.GetCompanyInfoByInn(inn);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var items = JsonConvert.DeserializeObject<Items>(await response.Content.ReadAsStringAsync());

                        companyInfo = items.ToCompanyInfoDTO(inn);
                        if (companyInfo == null) companyInfo = new CompanyInfoDTO()
                        {
                            INN = inn,
                            ResponseDescription = "Компания с указанным INN не найдена"
                        };

                    }
                    else if (response != null && response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        companyInfo = new CompanyInfoDTO()
                        {
                            INN = inn,
                            ResponseDescription = "Сервис по предоставлению информации о компаниях недоступен"
                        };
                    }
                    companyInfoList.Add(companyInfo!);
                }
                await _commandService.UpdateLastCommand(chatId, CommandNames.InnCommand, inns);
                return JsonConvert.SerializeObject(companyInfoList);
            }
            catch (Exception ex)
            {
                return $"Ошибка сервера: {ex.Message}";
            }
        }

        public async Task<string> FullCommand(long chatId, string inn)
        {
            try
            {
                if (string.IsNullOrEmpty(inn)) return "Некорректные параметры";
                var response = await _companyInformationService.GetCompanyInfoByInn(inn);
                await _commandService.UpdateLastCommand(chatId, CommandNames.FullCommand, inn);
                if (response != null && response.IsSuccessStatusCode)
                {
                    string result_str = "";
                    var items = JsonConvert.DeserializeObject<ItemsFull>(await response.Content.ReadAsStringAsync());
                    if (items?.ItemsList?.Count > 0)
                    {
                        var organization = items.ItemsList[0].LE;
                        if (organization != null)
                        {
                            return JsonConvert.SerializeObject(organization);
                        }
                    }
                    return "Компания с указанным ИНН не найдена";
                }

                return "Сервис по предоставлению информации о компаниях недоступен";
            }
            catch (Exception ex)
            {
                return $"Ошибка сервера: {ex.Message}";
            }
        }

        public async Task<object> EgrulCommand(long chatId, string inn)
        {
            try
            {
                if (string.IsNullOrEmpty(inn)) return "Некорректные параметры";
                var response = await _companyInformationService.GetPdfFileWithExtract(inn);
                await _commandService.UpdateLastCommand(chatId, CommandNames.EgrulCommand, inn);
                if (response.IsSuccessStatusCode)
                {
                    var contentType = response.Content.Headers.ContentType?.MediaType;
                    if (contentType == "application/pdf")
                    {
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                    return "Компания с указанным ИНН не найдена";
                }

                return "Сервис по предоставлению информации о компаниях недоступен";

            }
            catch (Exception ex)
            {
                return $"Ошибка сервера: {ex.Message}";
            }
        }

        public async Task<Command?> LastCommand(long chatId)
        {
            var command = await _commandService.GetLastCommand(chatId);
            return command;
        }
    }
}
