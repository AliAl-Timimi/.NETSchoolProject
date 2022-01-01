#nullable enable
using System.ComponentModel.DataAnnotations;

namespace Languages.BL.Domain
{
    public class Software
    {
        public Software()
        {
        }

        public Software(string name, string description, Language l)
        {
            Name = name;
            Description = description;
            LanguageUsed = l;
        }

        [Key] public long Id { get; set; }

        [Required] public string Name { get; set; }

        public string Description { get; set; }

        public Language? LanguageUsed { get; set; }

        public override string ToString()
        {
            return $"{Name,-10} {Description,-15}";
        }
    }
}