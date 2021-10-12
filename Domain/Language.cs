using System;
using System.Collections.Generic;

namespace Project.BL.Domain
{
    public class Language
    {
        public string Name { get; set; }
        public LanguageType Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Version { get; set; }
        public List<IDE>  Ides { get; set; }

        public Language(string name, LanguageType type, DateTime releaseDate, double version)
        {
            Name = name;
            Type = type;
            ReleaseDate = releaseDate;
            Version = version;
        }

        public override string ToString()
        {
            return $"{Type,4} {Name,10} (released {ReleaseDate:dd/MM/yyyy}) current version: {Version,5}";
            // return Type + " " + Name + " (released " + FormattedRelease() + ") current version: " + Version;
        }
    }
}