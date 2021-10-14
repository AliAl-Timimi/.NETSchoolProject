using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Project.BL.Domain;
using static Project.BL.Domain.LanguageType;

namespace Project.DAL
{
    public class InMemoryRepository : IRepository
    {
        private List<Language> _languages = new();
        private List<IDE> _ides = new();

        public InMemoryRepository()
        {
            Seed();
        }

        private void Seed()
        {
            CreateLanguage(new Language("Java", Oopl, new DateTime(1996, 1, 23), 16.02));
            CreateLanguage(new Language("C#", Oopl, new DateTime(2002, 1, 1), 9.0));
            CreateLanguage(new Language("Python", Fpl, new DateTime(1991, 2, 20), 3.76));
            CreateLanguage(new Language("C", Ppl, new DateTime(1972, 1, 1), 17));
            CreateLanguage(new Language("JavaScript", Oopl, new DateTime(1995, 12, 4), 12.0));

            CreateIde(new IDE("VSCode", "Microsoft", new DateTime(2015, 4, 29), 5, 0.00));
            CreateIde(new IDE("CLion", "JetBrains", new DateTime(2015, 4, 14), 1, 71.50));
            CreateIde(new IDE("IntelliJ", "JetBrains", new DateTime(2019, 12, 12), 2, 300.25));
            CreateIde(new IDE("PyCharm", "JetBrains", new DateTime(2010, 2, 3), 2, 119.99));
            CreateIde(new IDE("Rider", "JetBrains", new DateTime(2017, 2, 4), 1, 83.59));

            _languages[0].Ides = new List<IDE> {_ides[0], _ides[2]};
            _languages[1].Ides = new List<IDE> {_ides[0], _ides[4]};
            _languages[2].Ides = new List<IDE> {_ides[0], _ides[3]};
            _languages[3].Ides = new List<IDE> {_ides[0], _ides[1]};
            _languages[4].Ides = new List<IDE> {_ides[0], _ides[2], _ides[3]};

            _ides[0].Languages = new List<Language>
                {_languages[0], _languages[1], _languages[2], _languages[3], _languages[4]};
            _ides[1].Languages = new List<Language> {_languages[3]};
            _ides[2].Languages = new List<Language> {_languages[0], _languages[4]};
            _ides[3].Languages = new List<Language> {_languages[2], _languages[4]};
            _ides[4].Languages = new List<Language> {_languages[1]};
        }

        public IDE ReadIde(long id)
        {
            return _ides.FirstOrDefault(ide => ide.Id == id);
        }

        public Language ReadLanguage(long id)
        {
            return _languages.FirstOrDefault(language => language.Id == id);
        }

        public ICollection<IDE> ReadAllIdes()
        {
            return _ides;
        }

        public ICollection<Language> ReadAllLanguages()
        {
            return _languages;
        }

        public ICollection<Language> ReadLanguageByGenre(string type)
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

        public ICollection<IDE> ReadIdeByNameAndReleaseYear(string name, int year)
        {
            List<IDE> tempIdes = new List<IDE>();

            foreach (IDE ide in _ides)
            {
                if ((name.Length == 0 || ide.Name.ToLower().Contains(name.ToLower())) &&
                    (year == 0 || ide.ReleaseDate.Year == year))
                {
                    tempIdes.Add(ide);
                }
            }

            return tempIdes;
        }

        public void CreateIde(IDE ide)
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