using System;
using System.Collections.Generic;

namespace Project.BL.Domain
{
    public class IDE
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ReleaseDate { get;}
        public ICollection<Language> Languages { get; set; }
        public int SupportedLanguages { get; set; }
        public double? Price { get; set; }

        public IDE( string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price)
        {
            Name = name;
            Manufacturer = manufacturer;
            ReleaseDate = releaseDate;
            SupportedLanguages = supportedLanguages;
            Price = price;
        }
        
        public override string ToString()
        {
            return
                $"{Name,-10} created by {Manufacturer,10} (released {ReleaseDate:dd/MM/yyyy}) for {SupportedLanguages,2} language(s) price: {Price,2}";
            // return Name + " created by" + Manufacturer + " (released " + FormattedRelease() + ") for "+ SupportedLanguages  +" languages price: " + Price;
        }
    }
}