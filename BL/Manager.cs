using System;
using System.Collections.Generic;
using Project.BL.Domain;
using Project.DAL;

namespace Project.BL
{
    public class Manager : IManager
    {
        private IRepository _repository;

        public Manager()
        {
            _repository = new InMemoryRepository();
        }

        public IDE GetIde(long id)
        {
            return _repository.ReadIde(id);
        }

        public Language GetLanguage(long id)
        {
            return _repository.ReadLanguage(id);
        }

        public ICollection<IDE> GetAllIdes()
        {
            return _repository.ReadAllIdes();
        }

        public ICollection<Language> GetAllLanguages()
        {
            return _repository.ReadAllLanguages();
        }

        public ICollection<Language> GetLanguageByGenre(string type)
        {
            return _repository.ReadLanguageByGenre(type);
        }

        public ICollection<IDE> GetIdeByNameAndReleaseYear(string name, int releaseDate)
        {
            return _repository.ReadIdeByNameAndReleaseYear(name, releaseDate);
        }

        public void AddIde(long id, string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price)
        {
            _repository.CreateIde(new IDE(name, manufacturer, releaseDate, supportedLanguages, price));
        }

        public void AddLanguage(long id, string name, LanguageType type, DateTime releaseDate, double version)
        {
            _repository.CreateLanguage(new Language(name, type, releaseDate, version));
        }
    }
}