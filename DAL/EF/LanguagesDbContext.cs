using System;
using System.Diagnostics;
using Languages.BL.Domain;
using Microsoft.EntityFrameworkCore;

namespace Languages.DAL.EF
{
    public class LanguagesDbContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<Ide> Ides { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlite("Data Source=../../../../Languages.db");
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        public LanguagesDbContext()
        {
            LanguagesInitializer.Initialize(this,true);
        }
    }
}