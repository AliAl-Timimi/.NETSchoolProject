using System;
using System.Diagnostics;
using Languages.BL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Languages.DAL.EF
{
    public class LanguagesDbContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<Ide> Ides { get; set; }
        public DbSet<Software> Softwares { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlite("Data Source=../../../../Languages.db").LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }

        public LanguagesDbContext()
        {
            LanguagesInitializer.Initialize(this, true);
        }
    }
}