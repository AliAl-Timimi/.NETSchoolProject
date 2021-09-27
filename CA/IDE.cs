using System;
using System.Threading.Channels;

namespace CA
{
    public class IDE
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Language[] Languages { get; set; }
        public int SupportedLanguages { get; set; }
        public Nullable<double> Price { get; set; }

        public IDE(string name, string manufacturer, DateTime releaseDate, Language[] languages, int supportedLanguages, double? price)
        {
            Name = name;
            Manufacturer = manufacturer;
            ReleaseDate = releaseDate;
            Languages = languages;
            SupportedLanguages = supportedLanguages;
            Price = price;
        }
        
        

        private string FormattedRelease()
        {
            return $"{ReleaseDate:dd/MM/yyyy}";
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
            return Name + " created by" + Manufacturer + " (released " + FormattedRelease() + ") for "+ SupportedLanguages  +" languages price: " + Price;
        }
    }
}