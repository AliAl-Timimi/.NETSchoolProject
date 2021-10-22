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
        private ICollection<Language> _languages = new List<Language>();
        private ICollection<Ide> _ides = new List<Ide>();

        public InMemoryRepository()
        {
            Seed();
        }
        
        private void Seed()
        {
            Language java = new Language("Java", Oopl, new DateTime(1996, 1, 23), 16.02);
            Language csharp = new Language("C#", Oopl, new DateTime(2002, 1, 1), 9.0);
            Language python = new Language("Python", Fpl, new DateTime(1991, 2, 20), 3.76);
            Language c = new Language("C", Ppl, new DateTime(1972, 1, 1), 17);
            Language js = new Language("JavaScript", Oopl, new DateTime(1995, 12, 4), 12.0);

            Ide vscode = new Ide("VSCode", "Microsoft", new DateTime(2015, 4, 29), 5, null);
            Ide clion = new Ide("CLion", "JetBrains", new DateTime(2015, 4, 14), 1, 71.50);
            Ide intellij = new Ide("IntelliJ", "JetBrains", new DateTime(2019, 12, 12), 2, 300.25);
            Ide pycharm = new Ide("PyCharm", "JetBrains", new DateTime(2010, 2, 3), 2, 119.99);
            Ide rider= new Ide("Rider", "JetBrains", new DateTime(2017, 2, 4), 1, 83.59);

            java.Ides = new List<Ide> {vscode, intellij};
            csharp.Ides = new List<Ide> {vscode, rider};
            python.Ides = new List<Ide> {vscode, pycharm};
            c.Ides = new List<Ide> {vscode, clion};
            js.Ides = new List<Ide> {vscode, intellij, pycharm};

            vscode.Languages = new List<Language> {java, csharp, python, c, js};
            clion.Languages = new List<Language> {c};
            intellij.Languages = new List<Language> {java, js};
            pycharm.Languages = new List<Language> {python, js};
            rider.Languages = new List<Language> {csharp};


            IEnumerable<Language> languages = new List<Language> {java, csharp, python, c, js};
            foreach (Language language in languages) CreateLanguage(language);
            
            IEnumerable<Ide> ides = new List<Ide> {vscode, clion, intellij, pycharm, rider};
            foreach (Ide ide in ides) CreateIde(ide);
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

        public IEnumerable<Language> ReadLanguageByGenre(string type)
        {
            ICollection<Language> tempLanguages = new Collection<Language>();
            foreach (Language language in _languages)
            {
                if (language.Type.ToString() == type)
                {
                    tempLanguages.Add(language);
                }
            }

            return tempLanguages;
        }

        public IEnumerable<Ide> ReadIdeByNameAndReleaseYear(string name, int year)
        {
            List<Ide> tempIdes = new List<Ide>();

            foreach (Ide ide in _ides)
            {
                if ((name.Length == 0 || ide.Name.ToLower().Contains(name.ToLower())) &&
                    (year == 0 || ide.ReleaseDate.Year == year))
                {
                    tempIdes.Add(ide);
                }
            }

            return tempIdes;
        }

        public void CreateIde(Ide ide)
        {
            _ides.Add(ide);
            ide.Id = _ides.Count;
        }

        public void CreateLanguage(Language language)
        {
            _languages.Add(language);
            language.Id = _languages.Count;
        }
    }
}