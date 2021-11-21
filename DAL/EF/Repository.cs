using System.Collections.Generic;
using System.Linq;
using Languages.BL.Domain;

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

        public bool CreateIde(Ide ide)
        {
            _context.Ides.Add(ide);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return true;
        }

        public bool CreateLanguage(Language language)
        {
            _context.Languages.Add(language);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return true;
        }
    }
}