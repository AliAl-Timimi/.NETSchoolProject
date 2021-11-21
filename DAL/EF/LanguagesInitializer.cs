using System;
using System.Collections.Generic;
using Languages.BL.Domain;
using static Languages.BL.Domain.LanguageType;

namespace Languages.DAL.EF
{
    internal static class LanguagesInitializer
    {
        private static bool _isInitialized;

        public static void Initialize(LanguagesDbContext context, bool dropCreatedDatabase = false)
        {
            if (!_isInitialized)
            {
                if (dropCreatedDatabase) context.Database.EnsureDeleted();
                if (context.Database.EnsureCreated()) Seed(context);
                _isInitialized = true;
            }
        }

        private static void Seed(LanguagesDbContext context)
        {
            Language java = new Language("Java", Oopl, new DateTime(1996, 1, 23), 16.02);
            Language csharp = new Language("C#", Oopl, new DateTime(2002, 1, 1), 9.0);
            Language python = new Language("Python", Fpl, new DateTime(1991, 2, 20), 3.76);
            Language c = new Language("C", Ppl, new DateTime(1972, 1, 1), 17);
            Language js = new Language("JavaScript", Oopl, new DateTime(1995, 12, 4), 12.0);

            Ide vscode = new Ide("VSCode", "Microsoft", new DateTime(2015, 4, 29), 5, null);
            Ide clion = new Ide("CLion", "JetBrains", new DateTime(2015, 4, 14), 1, 71.50);
            Ide intellij = new Ide("IntelliJ", "JetBrains", new DateTime(2019, 12, 12), 2, 300.25);
            Ide pycharm = new Ide("PyCharm", "JetBrains", new DateTime(2010, 2, 3), 2, 119.99);
            Ide rider = new Ide("Rider", "JetBrains", new DateTime(2017, 2, 4), 1, 83.59);

            java.Ides = new List<Ide> {vscode, intellij};
            csharp.Ides = new List<Ide> {vscode, rider};
            python.Ides = new List<Ide> {vscode, pycharm};
            c.Ides = new List<Ide> {vscode, clion};
            js.Ides = new List<Ide> {vscode, intellij, pycharm};

            vscode.Languages = new List<Language> {java, csharp, python, c, js};
            clion.Languages = new List<Language> {c};
            intellij.Languages = new List<Language> {java, js};
            pycharm.Languages = new List<Language> {python, js};
            rider.Languages = new List<Language> {csharp};

            List<Language> languages = new() {java, csharp, python, c, js};
            List<Ide> ides = new() {vscode, clion, intellij, pycharm, rider};

            context.Languages.AddRange(languages);
            context.Ides.AddRange(ides);

            context.SaveChanges();
            context.ChangeTracker.Clear();
        }
    }
}