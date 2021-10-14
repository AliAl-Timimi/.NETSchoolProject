using System;
using System.Collections.Generic;

namespace Project.BL.Domain
{
    public class Language
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public LanguageType Type { get; set; }
        public DateTime ReleaseDate { get;}
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