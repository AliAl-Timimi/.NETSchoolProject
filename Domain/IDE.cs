using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace Project.BL.Domain
{
    public class IDE
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Language> Languages { get; set; }
        public int SupportedLanguages { get; set; }
        public double? Price { get; set; }

        public IDE(string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price)
        {
            Name = name;
            Manufacturer = manufacturer;
            ReleaseDate = releaseDate;
            SupportedLanguages = supportedLanguages;
            Price = price;
        }
        
        public void PrintLanguages()
        {
            Console.WriteLine("Supported languages for " + Name + ":");
            for (int i = 0; i < SupportedLanguages; i++)
            {
                Console.WriteLine(Languages[i]);
            }
        }

        public override string ToString()
        {
            return
                $"{Name} created by {Manufacturer} (released {ReleaseDate:dd/MM/yyyy}) for {SupportedLanguages} language(s) price: {Price,2}";
            // return Name + " created by" + Manufacturer + " (released " + FormattedRelease() + ") for "+ SupportedLanguages  +" languages price: " + Price;
        }
    }
}