using System;
using System.Collections;
using System.Collections.Generic;
using static CA.LanguageType;


namespace CA
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Language> languages = new List<Language>();
            List<IDE> ides = new List<IDE>();
            Seed(ref languages, ref ides);


            int input;
            int input2;
            bool cont;
            do
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("==========================");
                Console.WriteLine("0) Quit");
                Console.WriteLine("1) Show all Languages");
                Console.WriteLine("2) Show languages by type");
                Console.WriteLine("3) Show all IDEs");
                Console.WriteLine("4) Show IDEs with name and/or year of release");

                do
                {
                    Console.Write("Choice (1-4): ");
                    cont = int.TryParse(Console.ReadLine(), out input) && input >= 0 && input < 5;
                    if (!cont)
                    {
                        Console.WriteLine("Ongeldige invoer!");
                    }
                    Console.WriteLine();
                } while (!cont);

                switch (input)
                {
                    case 1:
                        Console.WriteLine("All languages");
                        Console.WriteLine("=============");
                        for (int i = 0; i < languages.Count; i++)
                        {
                            Console.WriteLine(languages[i]);
                        }
                        break;
                    case 2:
                        do
                        {
                            Console.Write("Language type (");
                            for (int i = 0; i < Enum.GetValues(typeof(LanguageType)).Length; i++)
                            {
                                Console.Write($"{i+1}={Enum.GetName(typeof(LanguageType), i)}");
                                if (i != Enum.GetValues(typeof(LanguageType)).Length-1)
                                {
                                    Console.Write(", ");
                                }
                            }
                            Console.Write("): ");
                            cont = int.TryParse(Console.ReadLine(), out input2) && input2 > 0 && input2 <= Enum.GetValues(typeof(LanguageType)).Length;
                            if (!cont)
                            {
                                Console.WriteLine("Ongeldige invoer!");
                            }
                        } while (!cont);

                        foreach (Language lang in languages)
                        {
                            if (lang.Type.ToString() == Enum.GetName(typeof(LanguageType), input2-1))
                            {
                                Console.WriteLine(lang);
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("All IDEs");
                        Console.WriteLine("========");
                        for (int i = 0; i < ides.Count; i++)
                        {
                            Console.WriteLine(ides[i]);
                        }

                        break;
                    case 4:
                        Console.Write("Enter (part of) a name or leave blank: ");
                        string search = Console.ReadLine();
                        string yearinput;
                        int year;
                        do
                        {
                            Console.Write("Enter a year or leave blank: ");
                            yearinput = Console.ReadLine();
                            if (!String.IsNullOrEmpty(yearinput))
                            {
                                cont = int.TryParse(yearinput, out year) && year <= DateTime.Now.Year;
                                if (!cont)
                                {
                                    Console.WriteLine("Ongeldige invoer!");
                                }
                            }
                            else
                            {
                                year = 0;
                                cont = true;
                            }
                        } while (!cont);

                        foreach (IDE ide in ides)
                        {
                            if ((search.Length == 0 || ide.Name.ToLower().Contains(search.ToLower())) &&
                                (year == 0 || ide.ReleaseDate.Year == year))
                            {
                                Console.WriteLine(ide);
                            }
                        }
                        break;
                }
                Console.WriteLine();
            } while (input != 0);
        }

        public static void Seed(ref List<Language> languages, ref List<IDE> ides)
        {
            languages.Add(new Language("Java", Oopl, new DateTime(1996, 1, 23), 16.02));
            languages.Add(new Language("C#", Oopl, new DateTime(2002, 1, 1), 9.0));
            languages.Add(new Language("Python", Fpl, new DateTime(1991, 2, 20), 3.76));
            languages.Add(new Language("C", Ppl, new DateTime(1972, 1, 1), 17));
            languages.Add(new Language("JavaScript", Oopl, new DateTime(1995, 12, 4), 12.0));

            ides.Add(new IDE("VSCode", "Microsoft", new DateTime(2015, 4, 29), 5, 0.00));
            ides.Add(new IDE("CLion", "JetBrains", new DateTime(2015, 4, 14), 1, 71.50));
            ides.Add(new IDE("IntelliJ", "JetBrains", new DateTime(2019, 12, 12), 2, 300.25));
            ides.Add(new IDE("PyCharm", "JetBrains", new DateTime(2010, 2, 3), 2, 119.99));
            ides.Add(new IDE("Rider", "JetBrains", new DateTime(2017, 2, 4), 1, 83.59));

            languages[0].Ides = new List<IDE> {ides[0], ides[2]};
            languages[1].Ides = new List<IDE> {ides[0], ides[4]};
            languages[2].Ides = new List<IDE> {ides[0], ides[3]};
            languages[3].Ides = new List<IDE> {ides[0], ides[1]};
            languages[4].Ides = new List<IDE> {ides[0], ides[2], ides[3]};
            
            ides[0].Languages = new List<Language>
                {languages[0], languages[1], languages[2], languages[3], languages[4]};
            ides[1].Languages = new List<Language> {languages[3]};
            ides[2].Languages = new List<Language> {languages[0], languages[4]};
            ides[3].Languages = new List<Language> {languages[2], languages[4]};
            ides[4].Languages = new List<Language> {languages[1]};
        }
    }
}