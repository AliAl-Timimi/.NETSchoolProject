using System.Diagnostics;
using Languages.BL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Languages.DAL.EF
{
    public class LanguagesDbContext : DbContext
    {
        public LanguagesDbContext()
        {
            LanguagesInitializer.Initialize(this, true);
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Ide> Ides { get; set; }
        public DbSet<IdeLanguage> IdeLanguages { get; set; }
        public DbSet<Software> Softwares { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlite("Data Source=../Languages.db")
                .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdeLanguage>().HasOne(il => il.Ide).WithMany(ide => ide.Languages)
                .HasForeignKey("IdeFK_shadow").OnDelete(DeleteBehavior.Cascade).IsRequired();
            modelBuilder.Entity<IdeLanguage>().HasOne(il => il.Language).WithMany(lang => lang.Ides)
                .HasForeignKey("LanguageFK_shadow").OnDelete(DeleteBehavior.Cascade).IsRequired();
            modelBuilder.Entity<Software>().HasOne(s => s.LanguageUsed).WithMany(lang => lang.Programs)
                .HasForeignKey("LanguageSFK_shadow").OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IdeLanguage>().ToTable("tblIdeLanguage").HasKey("IdeFK_shadow", "LanguageFK_shadow");
            modelBuilder.Entity<Ide>().ToTable("tblIdes").HasKey(ide => ide.Id);
            modelBuilder.Entity<Language>().ToTable("tblLanguages").HasKey(lang => lang.Id);
            modelBuilder.Entity<Software>().ToTable("tblSoftware").HasKey(s => s.Id);
        }
    }
}