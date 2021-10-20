using System;
using System.Collections.Generic;
using Languages.BL.Domain;

namespace Languages.BL
{
    public interface IManager
    {
        public IDE GetIde(long id);
        public Language GetLanguage(long id);
        public IEnumerable<IDE> GetAllIdes();
        public IEnumerable<Language> GetAllLanguages();
        public IEnumerable<Language> GetLanguageByGenre(string type);
        public IEnumerable<IDE> GetIdeByNameAndReleaseYear(string name, int releaseDate);
        public void AddIde(string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price);
        public void AddLanguage(string name, LanguageType type, DateTime releaseDate, double version);
    }
}