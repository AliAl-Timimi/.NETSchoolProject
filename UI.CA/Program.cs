using System;
using System.ComponentModel.DataAnnotations;
using Languages.BL;
using Languages.BL.Domain;

namespace Languages.UI.CA
{
    class Program
    {
        private readonly IManager _manager = new Manager();
        
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        private void Run()
        {
            int input;
            do
            {
                PrintMenu();
                bool cont;
                do
                {
                    Console.Write("Choice (1-6): ");
                    cont = int.TryParse(Console.ReadLine(), out input) && input >= 0 && input < 7;
                    if (!cont) Console.WriteLine("Ongeldige invoer!");
                    Console.WriteLine();
                } while (!cont);

                switch (input)
                {
                    case 1:
                        PrintLanguages();
                        break;
                    case 2:
                        LanguageByGenre();
                        break;
                    case 3:
                        PrintIdes();
                        break;
                    case 4:
                        IdeByNameAndYear();
                        break;
                    case 5:
                        AddLanguage();
                        break;
                    case 6:
                        AddIde();
                        break;
                }
                Console.WriteLine();
            } while (input != 0);
        }

        private void PrintMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("==========================");
            Console.WriteLine("0) Quit");
            Console.WriteLine("1) Show all Languages");
            Console.WriteLine("2) Show languages by type");
            Console.WriteLine("3) Show all IDEs");
            Console.WriteLine("4) Show IDEs with name and/or year of release");
            Console.WriteLine("5) Add a Language");
            Console.WriteLine("6) Add an IDE");
        }

        private void PrintLanguages()
        {
            Console.WriteLine("All languages");
            Console.WriteLine("=============");
            foreach (Language language in _manager.GetAllLanguages()) Console.WriteLine(language);
        }

        private void PrintIdes()
        {
            Console.WriteLine("All IDEs");
            Console.WriteLine("========");
            foreach (Ide ide in _manager.GetAllIdes()) Console.WriteLine(ide);
        }

        private void LanguageByGenre()
        {
            int input2;
            bool cont;
            do
            {
                PrintEnum();
                cont = int.TryParse(Console.ReadLine(), out input2) && input2 > 0 &&
                       input2 <= Enum.GetValues(typeof(LanguageType)).Length;
                if (!cont) Console.WriteLine("Ongeldige invoer!");
            } while (!cont);

            foreach (Language lang in _manager.GetLanguageByGenre(input2 - 1))
            {
                Console.WriteLine(lang);
            }
        }

        private void PrintEnum()
        {
            Console.Write("Language type (");
            for (int i = 0; i < Enum.GetValues(typeof(LanguageType)).Length; i++)
            {
                Console.Write($"{i + 1}={Enum.GetName(typeof(LanguageType), i)}");
                if (i != Enum.GetValues(typeof(LanguageType)).Length - 1)
                    Console.Write(", ");
            }

            Console.Write("): ");
        }

        private void IdeByNameAndYear()
        {
            bool cont;
            Console.Write("Enter (part of) a name or leave blank: ");
            string search = Console.ReadLine();
            int year = 0;
            do
            {
                Console.Write("Enter a year or leave blank: ");
                string yearinput = Console.ReadLine();
                if (!string.IsNullOrEmpty(yearinput))
                {
                    cont = int.TryParse(yearinput, out year) && year <= DateTime.Now.Year;
                    if (!cont) Console.WriteLine(year + "\nOngeldige invoer!");
                }
                else cont = true;
            } while (!cont);

            foreach (Ide ide in _manager.GetIdeByNameAndReleaseYear(search, year))
                Console.WriteLine(ide);
        }

        private void AddLanguage()
        {
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("Add Language");
                Console.WriteLine("=======");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                PrintEnum();
                int.TryParse(Console.ReadLine(), out int typeInt);
                if (typeInt > Enum.GetValues(typeof(LanguageType)).Length)
                    typeInt = -1;

                LanguageType type = (LanguageType) typeInt - 1;

                Console.Write("Release date (yyyy/mm/dd): ");
                DateTime.TryParse(Console.ReadLine(), out DateTime release);

                Console.Write("Version: ");
                double.TryParse(Console.ReadLine(), out double version);

                try
                {
                    _manager.AddLanguage(name, type, release, version);
                    repeat = false;
                }
                catch (ValidationException e)
                {
                    Console.WriteLine("Error: " + e.Message + "\nPlease try again...\n");
                    AddLanguage();
                }
            }
        }

        private void AddIde()
        {
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("Add IDE");
                Console.WriteLine("=======");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Manufacturer: ");
                string manufacturer = Console.ReadLine();

                Console.Write("Release date (yyyy/mm/dd): ");
                DateTime.TryParse(Console.ReadLine(), out DateTime release);

                Console.Write("Amount of supported languages: ");
                string amountSup = Console.ReadLine();
                int supportedLanguages;
                if (!string.IsNullOrEmpty(amountSup))
                    int.TryParse(amountSup, out supportedLanguages);
                else
                    supportedLanguages = -1;

                Console.Write("Price: ");
                double.TryParse(Console.ReadLine(), out double price);
                try
                {
                    _manager.AddIde(name, manufacturer, release, supportedLanguages, price);
                    repeat = false;
                }
                catch (ValidationException e)
                {
                    Console.WriteLine("Error: " + e.Message + "\nPlease try again...\n");
                }
            }
        }
    }
}