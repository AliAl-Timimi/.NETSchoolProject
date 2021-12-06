﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using Languages.BL;
using Languages.BL.Domain;
using Languages.DAL;
using Languages.DAL.EF;

namespace Languages.UI.CA
{
    class Program
    {
        private readonly IManager _manager;

        public Program(IManager manager)
        {
            _manager = manager;
        }

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            LanguagesDbContext context = new LanguagesDbContext();
            IRepository repository = new Repository(context);
            IManager manager = new Manager(repository);

            Program program = new Program(manager);
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
                    Console.Write("Choice (1-6): ");
                    Console.ForegroundColor = ConsoleColor.White;
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
        }

        private void PrintLanguages()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("All languages");
            Console.WriteLine("=============");
            Console.ResetColor();
            foreach (Language language in _manager.GetAllLanguages())
            {
                Console.WriteLine($"{language}", Console.ForegroundColor = ConsoleColor.Green);
                foreach (var software in _manager.GetAllSoftwaresWithLanguages())
                {
                    if (software.LanguageUsed == language)
                        Console.WriteLine($"{"Software:", 18}{software,31}", Console.ForegroundColor = ConsoleColor.Magenta);
                }
            }
        }

        private void PrintIdes()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("All IDEs");
            Console.WriteLine("========");
            foreach (Ide ide in _manager.GetAllIdesWithLanguages())
            {
                Console.WriteLine($"{ide}", Console.ForegroundColor = ConsoleColor.Cyan);
                foreach (IdeLanguage lang in ide.Languages)
                        Console.WriteLine($"{"Language:", 18}{lang.Language, 74}",Console.ForegroundColor = ConsoleColor.Green);
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

            foreach (Language lang in _manager.GetLanguageByGenre((LanguageType) (input2 - 1)))
                Console.WriteLine($"{lang}", Console.ForegroundColor = ConsoleColor.Green);
        }

        private void PrintEnum()
        {
            Console.Write("Language ");
            Console.Write($"type", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" (");
            for (int i = 0; i < Enum.GetValues(typeof(LanguageType)).Length; i++)
            {
                Console.Write($"{i + 1}=");
                Console.Write($"{Enum.GetName(typeof(LanguageType), i)}", Console.ForegroundColor = ConsoleColor.Yellow);
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
            string search = Console.ReadLine();
            int year = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter a year or leave blank: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string yearinput = Console.ReadLine();
                if (!string.IsNullOrEmpty(yearinput))
                {
                    cont = int.TryParse(yearinput, out year) && year <= DateTime.Now.Year;
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (!cont) Console.WriteLine(year + "\nOngeldige invoer!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else cont = true;
            } while (!cont);

            foreach (Ide ide in _manager.GetIdeByNameAndReleaseYear(search, year))
                Console.WriteLine($"{ide}", Console.ForegroundColor = ConsoleColor.Cyan);
        }

        private void AddLanguage()
        {
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("Add Language");
                Console.WriteLine("=======");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Name: ");
                
                Console.ForegroundColor = ConsoleColor.Blue;
                string name = Console.ReadLine();
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                PrintEnum();
                Console.ForegroundColor = ConsoleColor.Blue;
                int.TryParse(Console.ReadLine(), out int typeInt);
                if (typeInt > Enum.GetValues(typeof(LanguageType)).Length) typeInt = -1;
                
                LanguageType type = (LanguageType) typeInt - 1;
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Release date (yyyy/mm/dd): ");
                Console.ForegroundColor = ConsoleColor.Blue;
                DateTime.TryParse(Console.ReadLine(), out DateTime release);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Version: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                double.TryParse(Console.ReadLine(), out double version);

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
            bool repeat = true;
            while (repeat)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Add IDE");
                Console.WriteLine("=======");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Name: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string name = Console.ReadLine();
                
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Manufacturer: ");                
                Console.ForegroundColor = ConsoleColor.Blue;
                string manufacturer = Console.ReadLine();
                
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Release date (yyyy/mm/dd): ");
                Console.ForegroundColor = ConsoleColor.Blue;
                DateTime.TryParse(Console.ReadLine(), out DateTime release);
                
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Amount of supported languages: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string amountSup = Console.ReadLine();
                
                int supportedLanguages;
                if (!string.IsNullOrEmpty(amountSup))
                    int.TryParse(amountSup, out supportedLanguages);
                else supportedLanguages = -1;
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Price: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                double.TryParse(Console.ReadLine(), out double price);
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(_manager.AddIde(name, manufacturer, release, supportedLanguages, price) + " Successfully added");
                    repeat = false;
                }
                catch (ValidationException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: " + e.Message + "\nPlease try again...\n");
                }
            }
        }
    }
}