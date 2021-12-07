using System;
using System.Collections.Generic;
using System.Linq;
using Languages.BL.Domain;
using Microsoft.EntityFrameworkCore;

namespace Languages.DAL.EF
{
    public class Repository : IRepository
    {
        private readonly LanguagesDbContext _context;

        public Repository(LanguagesDbContext context)
        {
            _context = context;
        }

        public Ide ReadIde(long id)
        {
            return _context.Ides.Find(id);
        }

        public Language ReadLanguage(long id)
        {
            return _context.Languages.Find(id);
        }

        public IEnumerable<Ide> ReadAllIdes()
        {
            return _context.Ides.AsEnumerable();
        }

        public IEnumerable<Language> ReadAllLanguages()
        {
            return _context.Languages.AsEnumerable();
        }

        public IEnumerable<Language> ReadLanguageByGenre(LanguageType languageType)
        {
            return _context.Languages
                .Where(lang => lang.Type == languageType)
                .AsEnumerable();
        }

        public IEnumerable<Ide> ReadIdeByNameAndReleaseYear(string name, int releaseYear)
        {
            IQueryable<Ide> idesQueryable = _context.Ides;
            if (!string.IsNullOrEmpty(name))
                idesQueryable = idesQueryable.Where(ide => ide.Name.ToLower().Contains(name.ToLower()));
            if (releaseYear != 0)
                idesQueryable = idesQueryable.Where(ide => ide.ReleaseDate.Year == releaseYear);
            return idesQueryable.AsEnumerable();
        }

        public Ide CreateIde(Ide ide)
        {
            _context.Ides.Add(ide);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            IQueryable<Ide> idesQueryable = _context.Ides;
            idesQueryable = idesQueryable.Where(i => i.Name.ToLower() == ide.Name.ToLower());
            idesQueryable = idesQueryable.Where(i => i.ReleaseDate == ide.ReleaseDate);
            idesQueryable = idesQueryable.Where(i => (int) i.Price == (int) ide.Price);
            idesQueryable = idesQueryable.Where(i =>  i.SupportedLanguages == ide.SupportedLanguages);
            return idesQueryable.First();
        }

        public bool CreateLanguage(Language language)
        {
            _context.Languages.Add(language);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            
            return true;
        }

        public IEnumerable<Software> ReadAllSoftwaresWithLanguage()
        {
            return _context.Softwares.Include(s => s.LanguageUsed).AsEnumerable();
        }

        public IEnumerable<Ide> ReadAllIdesWithLanguages()
        {
            return _context.Ides
                .Include(i => i.Languages)
                .ThenInclude(c => c.Language)
                .AsEnumerable();
        }

        public void CreateIdeLanguage(IdeLanguage ideLanguage)
        {
            if (_context.IdeLanguages.Find(ideLanguage.Ide.Id, ideLanguage.Language.Id) != null)
                throw new ArgumentException("Language is already linked to Ide.");
            _context.IdeLanguages.Add(ideLanguage);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public void DeleteIdeLanguage(long ideId, long languageId)
        {
            if (_context.IdeLanguages.Find(ideId, languageId) == null)
                throw new ArgumentException("Language is not part of this ide.");
            _context.IdeLanguages.Remove(_context.IdeLanguages.Find(ideId, languageId));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public Ide ReadIdeWithLanguages(long id)
        {
            return _context.Ides
                .Include(i => i.Languages)
                .ThenInclude(c => c.Language)
                .FirstOrDefault(ide => ide.Id == id);
        }
        
        public Language ReadLanguageWithSoftware(long id)
        {
            return _context.Languages
                .Include(s => s.Programs)
                .Include(i => i.Ides)
                .ThenInclude(c => c.Language)
                .FirstOrDefault(ide => ide.Id == id);
        }
    }
}