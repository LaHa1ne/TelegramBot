using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.Bot;
using TelegramBot.DataLayer.Database;
using TelegramBot.Services.Interfaces;
using TelegramBot.Services.Services;

namespace TelegramBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();

            builder.Services.AddSingleton<TgBot>();

            builder.Services.AddTransient<ITelegramCommandService, TelegramCommandService>();
            builder.Services.AddTransient<ICompanyInformationService, CompanyInformationService>();
            builder.Services.AddTransient<ICommandService, CommandService>();

            builder.Services.AddHttpClient();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();
           

            app.MapControllers();

            app.Run();
        }
    }
}