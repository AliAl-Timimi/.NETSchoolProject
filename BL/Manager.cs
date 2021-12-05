using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Languages.BL.Domain;
using Languages.DAL;

namespace Languages.BL
{
    public class Manager : IManager
    {
        private readonly IRepository _repository;

        public Manager(IRepository repository)
        {
            _repository = repository;
        }

        public Ide GetIde(long id)
        {
            return _repository.ReadIde(id);
        }

        public Language GetLanguage(long id)
        {
            return _repository.ReadLanguage(id);
        }

        public IEnumerable<Ide> GetAllIdes()
        {
            return _repository.ReadAllIdes();
        }

        public IEnumerable<Language> GetAllLanguages()
        {
            return _repository.ReadAllLanguages();
        }

        public IEnumerable<Language> GetLanguageByGenre(LanguageType type)
        {
            return _repository.ReadLanguageByGenre(type);
        }

        public IEnumerable<Ide> GetIdeByNameAndReleaseYear(string name, int releaseDate)
        {
            return _repository.ReadIdeByNameAndReleaseYear(name, releaseDate);
        }

        public Ide AddIde(string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price)
        {
            Ide ide = new Ide(name, manufacturer, releaseDate, supportedLanguages, price);
            Validator.ValidateObject(ide, new ValidationContext(ide), true);
            return _repository.CreateIde(ide) ? ide : null;
        }

        public Language AddLanguage(string name, LanguageType type, DateTime releaseDate, double version)
        {
            Language lang = new Language(name, type, releaseDate, version);
            Validator.ValidateObject(lang, new ValidationContext(lang), true);
            return _repository.CreateLanguage(lang) ? lang : null;
        }

        public IEnumerable<Software> GetAllSoftwaresWithLanguages()
        {
            return _repository.ReadAllSoftwaresWithLanguage();
        }

        public IEnumerable<Ide> GetAllIdesWithLanguages()
        {
            return _repository.ReadAllIdesWithLanguages();
        }

    }
}