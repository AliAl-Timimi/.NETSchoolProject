using System;

namespace CA
{
    public class Language
    {
        public string Name { get; set; }
        public LanguageType Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Version { get; set; }
        public IDE[]  Ides { get; set; }

        public Language(string name, LanguageType type, DateTime releaseDate, double version, IDE[] ides)
        {
            Name = name;
            Type = type;
            ReleaseDate = releaseDate;
            Version = version;
            Ides = ides;
        }

        private string FormattedRelease() {
            return $"{ReleaseDate:dd/MM/yyyy}";
        }

        public override string ToString()
        {
            return Type + " " + Name + " (released " + FormattedRelease() + ") current version: " + Version;
        }
    }
}