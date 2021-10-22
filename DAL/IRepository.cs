using System;
using System.Collections.Generic;
using Languages.BL.Domain;

namespace Languages.DAL
{
    public interface IRepository
    {
        public Ide ReadIde(long id);
        public Language ReadLanguage(long id);
        public IEnumerable<Ide> ReadAllIdes();
        public IEnumerable<Language> ReadAllLanguages();
        public IEnumerable<Language> ReadLanguageByGenre(int languageType);
        public IEnumerable<Ide> ReadIdeByNameAndReleaseYear(string name, int releaseYear);
        public void CreateIde(Ide ide);
        public void CreateLanguage(Language language);
    }
}