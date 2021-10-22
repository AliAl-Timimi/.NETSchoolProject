using System;
using System.Collections.Generic;
using Languages.BL.Domain;

namespace Languages.BL
{
    public interface IManager
    {
        public Ide GetIde(long id);
        public Language GetLanguage(long id);
        public IEnumerable<Ide> GetAllIdes();
        public IEnumerable<Language> GetAllLanguages();
        public IEnumerable<Language> GetLanguageByGenre(int type);
        public IEnumerable<Ide> GetIdeByNameAndReleaseYear(string name, int releaseDate);
        public Ide AddIde(string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price);
        public Language AddLanguage(string name, LanguageType type, DateTime releaseDate, double version);
    }
}