using HistorySampleWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoHistoryCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace HistorySampleWebApp.Service
{
    public class DatabaseContext : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory
    = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
    }
}
