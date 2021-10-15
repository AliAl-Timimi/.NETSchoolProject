using System;
using System.Collections.Generic;
using Project.BL.Domain;

namespace Project.BL
{
    public interface IManager
    {
        public IDE GetIde(long id);
        public Language GetLanguage(long id);
        public ICollection<IDE> GetAllIdes();
        public ICollection<Language> GetAllLanguages();
        public ICollection<Language> GetLanguageByGenre(string type);
        public ICollection<IDE> GetIdeByNameAndReleaseYear(string name, int releaseDate);
        public void AddIde(string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price);
        public void AddLanguage(string name, LanguageType type, DateTime releaseDate, double version);
    }
}