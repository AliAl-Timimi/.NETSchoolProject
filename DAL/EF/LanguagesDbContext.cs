using System.Diagnostics;
using Languages.BL.Domain;
using Microsoft.EntityFrameworkCore;

namespace Languages.DAL.EF
{
    public class LanguagesDbContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<IDE> Ides { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlite("Data Source=Languages_Languages.db");
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        protected LanguagesDbContext()
        {
            LanguagesInitializer.Initialize(this,true);
        }
    }
}