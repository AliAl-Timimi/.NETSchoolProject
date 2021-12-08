using System.ComponentModel.DataAnnotations;

namespace Languages.BL.Domain
{
    public class IdeLanguage
    {
        public IdeLanguage()
        {
        }

        public IdeLanguage(Ide ide, Language language, int? popularityOrder)
        {
            Ide = ide;
            Language = language;
            PopularityOrder = popularityOrder;
        }

        public IdeLanguage(Ide ide, Language language)
        {
            Ide = ide;
            Language = language;
            PopularityOrder = 0;
        }

        [Required] public Ide Ide { get; set; }

        [Required] public Language Language { get; set; }

        public int? PopularityOrder { get; set; }
    }
}