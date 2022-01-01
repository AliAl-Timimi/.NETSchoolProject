using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Languages.BL.Domain;
using Languages.DAL;
using Languages.DAL.EF;

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

        public Ide AddIde(string name, string manufacturer, DateTime releaseDate, int? supportedLanguages,
            double? price)
        {
            var ide = new Ide(name, manufacturer, releaseDate, supportedLanguages, price ?? 0);
            Validator.ValidateObject(ide, new ValidationContext(ide), true);
            return _repository.CreateIde(ide);
        }

        public Language AddLanguage(string name, LanguageType type, DateTime releaseDate, double version)
        {
            var lang = new Language(name, type, releaseDate, version);
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

        public IdeLanguage AddLanguageToIde(long ideId, long langId)
        {
            var ideLanguage = new IdeLanguage(GetIde(ideId), GetLanguage(langId));
            _repository.CreateIdeLanguage(ideLanguage);
            return ideLanguage;
        }
        public IdeLanguage AddLanguageToIde(long ideId, long langId, int? popOrder)
        {
            var ideLanguage = new IdeLanguage(GetIde(ideId), GetLanguage(langId), popOrder);
            _repository.CreateIdeLanguage(ideLanguage);
            return ideLanguage;
        }

        public void RemoveLanguageFromIde(long ideId, long langId)
        {
            _repository.DeleteIdeLanguage(ideId, langId);
        }

        public Ide GetIdeWithLanguages(long id)
        {
            return _repository.ReadIdeWithLanguages(id);
        }

        public Language GetLanguageWithSoftware(long id)
        {
            return _repository.ReadLanguageWithSoftware(id);
        }

        public Software GetSoftware(long id)
        {
            return _repository.ReadSoftware(id);
        }

        public IEnumerable<Software> GetAllSoftware()
        {
            return _repository.ReadAllSoftware();
        }

        public Software AddSoftware(String name, String description)
        {
            return _repository.AddSoftware(new Software(name, description, null));
        }

        public void ChangeSoftware(Software software)
        {
            _repository.UpdateSoftware(software);
        }

        public Language GetLanguageWithIdes(long id)
        {
            return _repository.ReadLanguageWithIdes(id);
        }

        public IEnumerable<Ide> GetNonLinkedIdes(long id)
        {
            return _repository.ReadNonLinkedIdes(id);
        }
    }
}