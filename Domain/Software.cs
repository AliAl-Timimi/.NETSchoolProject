using System.ComponentModel.DataAnnotations;

namespace Languages.BL.Domain
{
    public class Software
    {
        [Key] public long id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public Language LanguageUsed { get; set; }

        public Software(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}