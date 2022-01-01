using System.Collections.Generic;
using Languages.BL.Domain;

namespace Languages.DAL
{
    public interface IRepository
    {
        public Ide ReadIde(long id);
        public Language ReadLanguage(long id);
        public IEnumerable<Ide> ReadAllIdes();
        public IEnumerable<Language> ReadAllLanguages();
        public IEnumerable<Language> ReadLanguageByGenre(LanguageType languageType);
        public IEnumerable<Ide> ReadIdeByNameAndReleaseYear(string name, int releaseYear);
        public Ide CreateIde(Ide ide);
        public bool CreateLanguage(Language language);
        public IEnumerable<Software> ReadAllSoftwaresWithLanguage();
        public IEnumerable<Ide> ReadAllIdesWithLanguages();
        public void CreateIdeLanguage(IdeLanguage ideLanguage);
        public void DeleteIdeLanguage(long ideId, long languageId);
        public Ide ReadIdeWithLanguages(long id);
        public Language ReadLanguageWithSoftware(long id);
        public Software ReadSoftware(long id);
        public IEnumerable<Software> ReadAllSoftware();
        public Software AddSoftware(Software software);
        public void UpdateSoftware(Software software);
        public Language ReadLanguageWithIdes(long id);
        public IEnumerable<Ide> ReadNonLinkedIdes(long id);
    }
}