using System;
using System.Collections.Generic;
using Languages.BL.Domain;

namespace Languages.DAL
{
    public interface IRepository
    {
        public IDE ReadIde(long id);
        public Language ReadLanguage(long id);
        public IEnumerable<IDE> ReadAllIdes();
        public IEnumerable<Language> ReadAllLanguages();
        public IEnumerable<Language> ReadLanguageByGenre(string languageType);
        public IEnumerable<IDE> ReadIdeByNameAndReleaseYear(string name, int releaseYear);
        public void CreateIde(IDE ide);
        public void CreateLanguage(Language language);
    }
}