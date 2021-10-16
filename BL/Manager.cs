using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Project.BL.Domain;
using Project.DAL;

namespace Project.BL
{
    public class Manager : IManager
    {
        private readonly IRepository _repository;

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

        public IEnumerable<IDE> GetAllIdes()
        {
            return _repository.ReadAllIdes();
        }

        public IEnumerable<Language> GetAllLanguages()
        {
            return _repository.ReadAllLanguages();
        }

        public IEnumerable<Language> GetLanguageByGenre(string type)
        {
            return _repository.ReadLanguageByGenre(type);
        }

        public IEnumerable<IDE> GetIdeByNameAndReleaseYear(string name, int releaseDate)
        {
            return  _repository.ReadIdeByNameAndReleaseYear(name, releaseDate);
        }

        public void AddIde(string name, string manufacturer, DateTime releaseDate, int supportedLanguages,
            double? price)
        {
            IDE ide = new IDE(name, manufacturer, releaseDate, supportedLanguages, price);
            Validator.ValidateObject(ide, new ValidationContext(ide), true);
            _repository.CreateIde(ide);
        }

        public void AddLanguage(string name, LanguageType type, DateTime releaseDate, double version)
        {
            Language lang = new Language(name, type, releaseDate, version);
            Validator.ValidateObject(lang, new ValidationContext(lang), true);
            _repository.CreateLanguage(lang);
        }
    }
}