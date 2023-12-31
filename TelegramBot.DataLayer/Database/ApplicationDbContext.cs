﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.DataLayer.Entities;

namespace TelegramBot.DataLayer.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Command> Commands { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
