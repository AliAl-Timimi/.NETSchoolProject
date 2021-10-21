using System;
using System.Collections.Generic;
using Languages.BL.Domain;
using static Languages.BL.Domain.LanguageType;

namespace Languages.DAL.EF
{
    public class LanguagesInitializer
    {
        public static void Initialize(LanguagesDbContext context, bool dropCreatedDatabase = false)
        {
            if (dropCreatedDatabase)
                context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated())
                Seed(context);
        }

        private static void Seed(LanguagesDbContext context)
        {
            Language java = new Language("Java", Oopl, new DateTime(1996, 1, 23), 16.02);
            Language csharp = new Language("C#", Oopl, new DateTime(2002, 1, 1), 9.0);
            Language python = new Language("Python", Fpl, new DateTime(1991, 2, 20), 3.76);
            Language c = new Language("C", Ppl, new DateTime(1972, 1, 1), 17);
            Language js = new Language("JavaScript", Oopl, new DateTime(1995, 12, 4), 12.0);

            IDE vscode = new IDE("VSCode", "Microsoft", new DateTime(2015, 4, 29), 5, null);
            IDE clion = new IDE("CLion", "JetBrains", new DateTime(2015, 4, 14), 1, 71.50);
            IDE intellij = new IDE("IntelliJ", "JetBrains", new DateTime(2019, 12, 12), 2, 300.25);
            IDE pycharm = new IDE("PyCharm", "JetBrains", new DateTime(2010, 2, 3), 2, 119.99);
            IDE rider= new IDE("Rider", "JetBrains", new DateTime(2017, 2, 4), 1, 83.59);

            java.Ides = new List<IDE> {vscode, intellij};
            csharp.Ides = new List<IDE> {vscode, rider};
            python.Ides = new List<IDE> {vscode, pycharm};
            c.Ides = new List<IDE> {vscode, clion};
            js.Ides = new List<IDE> {vscode, intellij, pycharm};

            vscode.Languages = new List<Language> {java, csharp, python, c, js};
            clion.Languages = new List<Language> {c};
            intellij.Languages = new List<Language> {java, js};
            pycharm.Languages = new List<Language> {python, js};
            rider.Languages = new List<Language> {csharp};

            context.Languages.AddRange(new List<Language> {java, csharp, python, c, js});
            context.Ides.AddRange(new List<IDE> {vscode, clion, intellij, pycharm, rider});
            context.SaveChanges();
            context.ChangeTracker.Clear();
        }

    }
}