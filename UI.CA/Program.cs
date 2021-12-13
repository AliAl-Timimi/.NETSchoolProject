using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Languages.BL;
using Languages.BL.Domain;
using Languages.DAL;
using Languages.DAL.EF;

namespace Languages.UI.CA
{
    internal class Program
    {
        private readonly IManager _manager;

        public Program(IManager manager)
        {
            _manager = manager;
        }

        private static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            var context = new LanguagesDbContext();
            IRepository repository = new Repository(context);
            IManager manager = new Manager(repository);

            var program = new Program(manager);
            program.Run();
        }

        private void Run()
        {
            int input;
            do
            {
                Console.ResetColor();
                PrintMenu();
                bool cont;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Choice (0-8): ");
                    Console.ForegroundColor = ConsoleColor.White;
                    cont = int.TryParse(Console.ReadLine(), out input) && input >= 0 && input < 9;
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
                    case 7:
                        AddLanguageToIde();
                        break;
                    case 8:
                        RemoveLanguageFromIde();
                        break;
                }

                Console.WriteLine();
            } while (input != 0);
        }

        private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("==========================");
            Console.WriteLine("0) Quit");
            Console.WriteLine("1) Show all Languages");
            Console.WriteLine("2) Show languages by type");
            Console.WriteLine("3) Show all IDEs");
            Console.WriteLine("4) Show IDEs with name and/or year of release");
            Console.WriteLine("5) Add a Language");
            Console.WriteLine("6) Add an IDE");
            Console.WriteLine("7) Add language to Ide");
            Console.WriteLine("8) Remove language from Ide");
        }

        private void PrintLanguages()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("All languages");
            Console.WriteLine("=============");
            Console.ResetColor();
            foreach (var language in _manager.GetAllLanguages())
            {
                Console.WriteLine($"{language}", Console.ForegroundColor = ConsoleColor.Green);
                foreach (var software in _manager.GetAllSoftwaresWithLanguages())
                    if (software.LanguageUsed == language)
                        Console.WriteLine($"{"Software:",18}{software,31}",
                            Console.ForegroundColor = ConsoleColor.Magenta);
            }
        }

        private void PrintIdes()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("All IDEs");
            Console.WriteLine("========");
            foreach (var ide in _manager.GetAllIdesWithLanguages())
            {
                Console.WriteLine($"{ide}", Console.ForegroundColor = ConsoleColor.Cyan);
                foreach (var lang in ide.Languages)
                    Console.WriteLine($"{"Language:",18}{lang.Language,74}",
                        Console.ForegroundColor = ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
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

            foreach (var lang in _manager.GetLanguageByGenre((LanguageType) (input2 - 1)))
                Console.WriteLine($"{lang}", Console.ForegroundColor = ConsoleColor.Green);
        }

        private void PrintEnum()
        {
            Console.Write("Language ");
            Console.Write("type", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" (");
            for (var i = 0; i < Enum.GetValues(typeof(LanguageType)).Length; i++)
            {
                Console.Write($"{i + 1}=");
                Console.Write($"{Enum.GetName(typeof(LanguageType), i)}",
                    Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ForegroundColor = ConsoleColor.White;
                if (i != Enum.GetValues(typeof(LanguageType)).Length - 1) Console.Write(", ");
            }

            Console.Write("): ");
        }

        private void IdeByNameAndYear()
        {
            bool cont;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter (part of) a name or leave blank: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            var search = Console.ReadLine();
            var year = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter a year or leave blank: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                var yearinput = Console.ReadLine();
                if (!string.IsNullOrEmpty(yearinput))
                {
                    cont = int.TryParse(yearinput, out year) && year <= DateTime.Now.Year;
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (!cont) Console.WriteLine(year + "\nOngeldige invoer!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    cont = true;
                }
            } while (!cont);

            foreach (var ide in _manager.GetIdeByNameAndReleaseYear(search, year))
                Console.WriteLine($"{ide}", Console.ForegroundColor = ConsoleColor.Cyan);
        }

        private void AddLanguage()
        {
            var repeat = true;
            while (repeat)
            {
                Console.WriteLine("Add Language");
                Console.WriteLine("=======");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Name: ");

                Console.ForegroundColor = ConsoleColor.Blue;
                var name = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                PrintEnum();
                Console.ForegroundColor = ConsoleColor.Blue;
                int.TryParse(Console.ReadLine(), out var typeInt);
                if (typeInt > Enum.GetValues(typeof(LanguageType)).Length) typeInt = -1;

                var type = (LanguageType) typeInt - 1;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Release date (yyyy/mm/dd): ");
                Console.ForegroundColor = ConsoleColor.Blue;
                DateTime.TryParse(Console.ReadLine(), out var release);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Version: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                double.TryParse(Console.ReadLine(), out var version);

                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(_manager.AddLanguage(name, type, release, version) + " Successfully added");
                    repeat = false;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (ValidationException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: " + e.Message + "\nPlease try again...\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        private void AddIde()
        {
            var repeat = true;
            while (repeat)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Add IDE");
                Console.WriteLine("=======");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Name: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                var name = Console.ReadLine();


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Manufacturer: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                var manufacturer = Console.ReadLine();


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Release date (yyyy/mm/dd): ");
                Console.ForegroundColor = ConsoleColor.Blue;
                DateTime.TryParse(Console.ReadLine(), out var release);


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Amount of supported languages: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                var amountSup = Console.ReadLine();

                int supportedLanguages;
                if (!string.IsNullOrEmpty(amountSup))
                    int.TryParse(amountSup, out supportedLanguages);
                else supportedLanguages = -1;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Price: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                double.TryParse(Console.ReadLine(), out var price);
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(_manager.AddIde(name, manufacturer, release, supportedLanguages, price) +
                                      " Successfully added");
                    repeat = false;
                }
                catch (ValidationException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: " + e.Message + "\nPlease try again...\n");
                }
            }
        }

        private void AddLanguageToIde()
        {
            var repeat = true;
            while (repeat)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Which Ide would you like to add a language to?");
                Console.WriteLine("==============================================");
                bool cont;
                int idIde;
                do
                {
                    Console.Write("[0] ", Console.ForegroundColor = ConsoleColor.Yellow);
                    Console.WriteLine("Cancel", Console.ForegroundColor = ConsoleColor.Magenta);
                    foreach (var ide in _manager.GetAllIdes())
                    {
                        Console.Write($"[{ide.Id}] ", Console.ForegroundColor = ConsoleColor.Yellow);
                        Console.WriteLine($"{ide}", Console.ForegroundColor = ConsoleColor.Cyan);
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter an ide ID: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    cont = int.TryParse(Console.ReadLine(), out idIde)
                           && idIde > 0 && idIde <= _manager.GetAllIdes().ToList().Count;
                    if (idIde == 0)
                        return;
                    if (!cont)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose a valid ide id.");
                    }
                } while (!cont);

                int idLang;
                do
                {
                    Console.Write("[0] ", Console.ForegroundColor = ConsoleColor.Yellow);
                    Console.WriteLine("Cancel", Console.ForegroundColor = ConsoleColor.Magenta);
                    foreach (var l in _manager.GetAllLanguages())
                    {
                        Console.Write($"[{l.Id}] ", Console.ForegroundColor = ConsoleColor.Yellow);
                        Console.WriteLine($"{l}", Console.ForegroundColor = ConsoleColor.Green);
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter a language ID: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    cont = int.TryParse(Console.ReadLine(), out idLang)
                           && idLang >= 0 && idLang <= _manager.GetAllLanguages().ToList().Count;
                    if (idLang == 0) return;
                    if (!cont)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose a valid language id.");
                    }
                } while (!cont);

                try
                {
                    var ideLanguage = _manager.AddLanguageToIde(idIde, idLang);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully added");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(ideLanguage.Language + "to");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(ideLanguage.Ide);
                    repeat = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}", Console.ForegroundColor = ConsoleColor.Red);
                }
            }
        }

        private void RemoveLanguageFromIde()
        {
            var repeat = true;
            while (repeat)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Which Ide would you like to remove a language from?");
                Console.WriteLine("==================================================");
                bool cont;
                int idIde;
                do
                {
                    Console.Write("[0] ", Console.ForegroundColor = ConsoleColor.Yellow);
                    Console.WriteLine("Cancel", Console.ForegroundColor = ConsoleColor.Magenta);
                    foreach (var ide in _manager.GetAllIdes())
                    {
                        Console.Write($"[{ide.Id}] ", Console.ForegroundColor = ConsoleColor.Yellow);
                        Console.WriteLine($"{ide}", Console.ForegroundColor = ConsoleColor.Cyan);
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter an ide ID: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    cont = int.TryParse(Console.ReadLine(), out idIde)
                           && idIde > 0 && idIde <= _manager.GetAllIdes().ToList().Count;
                    if (idIde == 0)
                        return;
                    if (!cont)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose a valid ide id.");
                    }
                } while (!cont);

                IEnumerable<IdeLanguage> langs = _manager.GetIdeWithLanguages(idIde).Languages;
                long idLang;
                do
                {
                    Console.Write("[0] ", Console.ForegroundColor = ConsoleColor.Yellow);
                    Console.WriteLine("Cancel", Console.ForegroundColor = ConsoleColor.Magenta);
                    for (int i = 1; i <= langs.Count(); i++)
                    {
                        Console.Write($"[{i}] ", Console.ForegroundColor = ConsoleColor.Yellow);
                        Console.WriteLine($"{langs.ElementAt(i-1).Language}", Console.ForegroundColor = ConsoleColor.Green);
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter a language ID: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    cont = long.TryParse(Console.ReadLine(), out idLang)
                           && idLang >= 0 && idLang <= langs.Count();
                    if (idLang == 0) return;
                    if (!cont)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose a valid language id.");
                    }
                } while (!cont);

                idLang = langs.ElementAt((int) idLang).Language.Id;

                try
                {
                    _manager.RemoveLanguageFromIde(idIde, idLang);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully removed.");
                    repeat = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}", Console.ForegroundColor = ConsoleColor.Red);
                }
            }
        }
    }
}