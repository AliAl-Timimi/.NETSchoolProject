using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Languages.BL.Domain
{
    public class Language
    {
        [Key] public long Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Name can not be over 15 characters long.")]
        public string Name { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "A valid option must be chosen from the menu.")]
        public LanguageType Type { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Range(0.00001, double.PositiveInfinity, ErrorMessage = "Version needs to be filled and above 0")]
        public double Version { get; set; }

        public ICollection<IdeLanguage> Ides { get; set; }
        public ICollection<Software> Programs { get; set; }

        public Language(string name, LanguageType type, DateTime releaseDate, double version)
        {
            Name = name;
            Type = type;
            ReleaseDate = releaseDate;
            Version = version;
        }
        
        public Language(string name, LanguageType type, DateTime releaseDate, double version, ICollection<IdeLanguage> ides)
        {
            Name = name;
            Type = type;
            ReleaseDate = releaseDate;
            Version = version;
            Ides = ides;
        }

        public override string ToString()
        {
            return $"{Type,4} {Name,-15} (released {ReleaseDate:dd/MM/yyyy}) current version: {Version,5}";
        }
    }
}