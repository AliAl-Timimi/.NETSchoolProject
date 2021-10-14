using System;
using System.Collections.Generic;
using Project.BL.Domain;

namespace Project.DAL
{
    public interface IRepository
    {
        public IDE ReadIde(long id);
        public Language ReadLanguage(long id);
        public ICollection<IDE> ReadAllIdes();
        public ICollection<Language> ReadAllLanguages();
        public ICollection<Language> ReadLanguageByGenre(string languageType);
        public ICollection<IDE> ReadIdeByNameAndReleaseYear(string name, int releaseYear);
        public void CreateIde(IDE ide);
        public void CreateLanguage(Language language);
    }
}