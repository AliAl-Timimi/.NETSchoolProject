using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.BL.Domain
{
    public class Language
    {
        public long Id { get; set; }
        
        [StringLength(50,ErrorMessage = "Name can not be over 50 characters long.")] 
        public string Name { get; set; }
        [Required]
        public LanguageType Type { get; set; }
        [Required]
        public DateTime ReleaseDate { get;}
        [Required]
        public double Version { get; set; }
        public ICollection<IDE>  Ides { get; set; }

        public Language( string name, LanguageType type, DateTime releaseDate, double version)
        {
            Name = name;
            Type = type;
            ReleaseDate = releaseDate;
            Version = version;
        }

        public override string ToString()
        {
            return $"{Type,4} {Name,-10} (released {ReleaseDate:dd/MM/yyyy}) current version: {Version,5}";
        }
    }
}