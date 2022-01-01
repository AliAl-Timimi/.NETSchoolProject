using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Languages.BL.Domain;
using static Languages.BL.Domain.LanguageType;

namespace Languages.DAL
{
    public class InMemoryRepository : IRepository
    {
        private readonly ICollection<Ide> _ides = new List<Ide>();
        private readonly ICollection<Language> _languages = new List<Language>();

        public InMemoryRepository()
        {
            Seed();
        }

        public Ide ReadIde(long id)
        {
            return _ides.FirstOrDefault(ide => ide.Id == id);
        }

        public Language ReadLanguage(long id)
        {
            return _languages.FirstOrDefault(language => language.Id == id);
        }

        public IEnumerable<Ide> ReadAllIdes()
        {
            return _ides;
        }

        public IEnumerable<Language> ReadAllLanguages()
        {
            return _languages;
        }

        public IEnumerable<Language> ReadLanguageByGenre(LanguageType type)
        {
            ICollection<Language> tempLanguages = new Collection<Language>();
            foreach (var language in _languages)
                if (language.Type.ToString() == type.ToString())
                    tempLanguages.Add(language);
            return tempLanguages;
        }

        public IEnumerable<Ide> ReadIdeByNameAndReleaseYear(string name, int year)
        {
            var tempIdes = new List<Ide>();

            foreach (var ide in _ides)
                if ((name.Length == 0 || ide.Name.ToLower().Contains(name.ToLower())) &&
                    (year == 0 || ide.ReleaseDate.Year == year))
                    tempIdes.Add(ide);
            return tempIdes;
        }

        public Ide CreateIde(Ide ide)
        {
            _ides.Add(ide);
            ide.Id = _ides.Count;
            return ide;
        }

        public bool CreateLanguage(Language language)
        {
            _languages.Add(language);
            language.Id = _languages.Count;
            return true;
        }

        public IEnumerable<Software> ReadAllSoftwaresWithLanguage()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ide> ReadAllIdesWithLanguages()
        {
            throw new NotImplementedException();
        }

        public void CreateIdeLanguage(IdeLanguage ideLanguage)
        {
            throw new NotImplementedException();
        }

        public void DeleteIdeLanguage(long ideId, long languageId)
        {
            throw new NotImplementedException();
        }

        public Ide ReadIdeWithLanguages(long id)
        {
            throw new NotImplementedException();
        }

        public Language ReadLanguageWithSoftware(long id)
        {
            throw new NotImplementedException();
        }

        private void Seed()
        {
            // Language creation
            var java = new Language("Java", Oopl, new DateTime(1996, 1, 23), 16.02);
            var csharp = new Language("C#", Oopl, new DateTime(2002, 1, 1), 9.0);
            var python = new Language("Python", Fpl, new DateTime(1991, 2, 20), 3.76);
            var c = new Language("C", Ppl, new DateTime(1972, 1, 1), 17);
            var js = new Language("JavaScript", Oopl, new DateTime(1995, 12, 4), 12.0);

            // Ide creation
            var vscode = new Ide("VSCode", "Microsoft", new DateTime(2015, 4, 29), 5, 0);
            var clion = new Ide("CLion", "JetBrains", new DateTime(2015, 4, 14), 1, 71.50);
            var intellij = new Ide("IntelliJ", "JetBrains", new DateTime(2019, 12, 12), 2, 300.25);
            var pycharm = new Ide("PyCharm", "JetBrains", new DateTime(2010, 2, 3), 2, 119.99);
            var rider = new Ide("Rider", "JetBrains", new DateTime(2017, 2, 4), 1, 83.59);

            //Software create

            // IdeLanguage creation (linking class between Ide and Language with a popularity order for ordering languages inside Ide)
            var javaVscode = new IdeLanguage(vscode, java, 4);
            var csharpVscode = new IdeLanguage(vscode, csharp, 3);
            var pythonVscode = new IdeLanguage(vscode, python, 1);
            var cVscode = new IdeLanguage(vscode, c, 2);
            var jsVscode = new IdeLanguage(vscode, js, 5);
            var javaIntellij = new IdeLanguage(intellij, java, 1);
            var jsIntellij = new IdeLanguage(intellij, js, 2);
            var cClion = new IdeLanguage(clion, c, 1);
            var pythonPycharm = new IdeLanguage(pycharm, python, 1);
            var jsPycharm = new IdeLanguage(pycharm, js, 2);
            var csharpRider = new IdeLanguage(rider, csharp, 1);

            /*
            var spotify = new Software("Spotify", "Music streaming", java);
            var netflix = new Software("Netflix", "Video streaming", java);
            var instagram = new Software("Instagram", "Social media", python);
            var osu = new Software("Osu", "Game", csharp);
            var doom = new Software("Doom", "Game", c);
*/

            java.Ides = new List<IdeLanguage> {javaVscode, javaIntellij};
            csharp.Ides = new List<IdeLanguage> {csharpVscode, csharpRider};
            python.Ides = new List<IdeLanguage> {pythonVscode, pythonVscode};
            c.Ides = new List<IdeLanguage> {cVscode, cClion};
            js.Ides = new List<IdeLanguage> {jsVscode, jsIntellij, jsPycharm};

            vscode.Languages = new List<IdeLanguage> {javaVscode, csharpVscode, pythonVscode, cVscode, jsVscode};
            clion.Languages = new List<IdeLanguage> {cClion};
            intellij.Languages = new List<IdeLanguage> {javaIntellij, jsIntellij};
            pycharm.Languages = new List<IdeLanguage> {pythonPycharm, jsPycharm};
            rider.Languages = new List<IdeLanguage> {csharpRider};
            /*
            List<IdeLanguage> ideLanguages = new()
            {
                javaVscode, csharpVscode, pythonVscode, cVscode, jsVscode,
                javaIntellij, jsIntellij,
                cClion,
                pythonPycharm, jsPycharm,
                csharpRider
            };
            List<Software> software = new() {spotify, netflix, instagram, osu, doom};
            List<Language> languages = new() {java, csharp, python, c, js};
            List<Ide> ides = new() {vscode, clion, intellij, pycharm, rider};
            
            List<Language> languages = new() {java, csharp, python, c, js};
            List<Ide> ides = new() {vscode, clion, intellij, pycharm, rider};
            IEnumerable<Language> languages = new List<Language> {java, csharp, python, c, js};
            foreach (Language language in languages) CreateLanguage(language);

            IEnumerable<Ide> ides = new List<Ide> {vscode, clion, intellij, pycharm, rider};
            foreach (Ide ide in ides) CreateIde(ide);
            */
            
            
        }

        public Software ReadSoftware(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Software> ReadAllSoftware()
        {
            throw new NotImplementedException();
        }
        public Software AddSoftware(Software software)
        {
            throw new NotImplementedException();
        }
        public void UpdateSoftware(Software software)
        {
            throw new NotImplementedException();
        }

        public Language ReadLanguageWithIdes(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ide> ReadNonLinkedIdes(long id)
        {
            throw new NotImplementedException();
        }
    }
}