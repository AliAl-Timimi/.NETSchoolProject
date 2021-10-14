using System;
using Project.BL;
using Project.BL.Domain;

namespace Project.UI.CA
{
    class Program
    {
        static void Main(string[] args)
        {
            IManager manager = new Manager();
            int input;
            int input2;
            bool cont;
            do
            {
                PrintMenu();
                do
                {
                    Console.Write("Choice (1-4): ");
                    cont = int.TryParse(Console.ReadLine(), out input) && input >= 0 && input < 7;
                    if (!cont)
                    {
                        Console.WriteLine("Ongeldige invoer!");
                    }

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
                        
                        break;
                    case 6:
                        
                        break;
                }

                Console.WriteLine();
            } while (input != 0);

            void PrintMenu()
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("==========================");
                Console.WriteLine("0) Quit");
                Console.WriteLine("1) Show all Languages");
                Console.WriteLine("2) Show languages by type");
                Console.WriteLine("3) Show all IDEs");
                Console.WriteLine("4) Show IDEs with name and/or year of release");
            }
            
            void PrintLanguages()
            {
                Console.WriteLine("All languages");
                Console.WriteLine("=============");
                foreach (Language language in manager.GetAllLanguages())
                {
                    Console.WriteLine(language);
                }
            }

            void PrintIdes()
            {
                Console.WriteLine("All IDEs");
                Console.WriteLine("========");
                foreach (IDE ide in manager.GetAllIdes())
                {
                    Console.WriteLine(ide);
                }
            }

            void LanguageByGenre()
            {
                do
                {
                    Console.Write("Language type (");
                    for (int i = 0; i < Enum.GetValues(typeof(LanguageType)).Length; i++)
                    {
                        Console.Write($"{i + 1}={Enum.GetName(typeof(LanguageType), i)}");
                        if (i != Enum.GetValues(typeof(LanguageType)).Length - 1)
                        {
                            Console.Write(", ");
                        }
                    }

                    Console.Write("): ");
                    cont = int.TryParse(Console.ReadLine(), out input2) && input2 > 0 &&
                           input2 <= Enum.GetValues(typeof(LanguageType)).Length;
                    if (!cont)
                    {
                        Console.WriteLine("Ongeldige invoer!");
                    }
                } while (!cont);

                foreach (Language lang in manager.GetLanguageByGenre(Enum.GetName(typeof(LanguageType), input2 - 1)))
                {
                    Console.WriteLine(lang);
                }
            }

            void IdeByNameAndYear()
            {
                Console.Write("Enter (part of) a name or leave blank: ");
                string search = Console.ReadLine();
                string yearinput;
                int year = 0;
                do
                {
                    Console.Write("Enter a year or leave blank: ");
                    yearinput = Console.ReadLine();
                    if (!String.IsNullOrEmpty(yearinput))
                    {
                        cont = int.TryParse(yearinput, out year) && year <= DateTime.Now.Year;
                        if (!cont)
                        {
                            Console.WriteLine(year);
                            Console.WriteLine("Ongeldige invoer!");
                        }
                    }
                    else
                    {
                        cont = true;
                    }
                } while (!cont);

                foreach (IDE ide in manager.GetIdeByNameAndReleaseYear(search, year))
                {
                    Console.WriteLine(ide);
                }
                
            }
        }
    }
}