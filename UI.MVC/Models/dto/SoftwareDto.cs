#nullable enable
using Languages.BL.Domain;

namespace Languages.UI.MVC.Models.dto
{
    public class SoftwareDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Language? LanguageUsed { get; set; }
    }
}