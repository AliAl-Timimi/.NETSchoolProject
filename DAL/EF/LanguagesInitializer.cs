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
            // Language creation
            Language java = new Language("Java", Oopl, new DateTime(1996, 1, 23), 16.02);
            Language csharp = new Language("C#", Oopl, new DateTime(2002, 1, 1), 9.0);
            Language python = new Language("Python", Fpl, new DateTime(1991, 2, 20), 3.76);
            Language c = new Language("C", Ppl, new DateTime(1972, 1, 1), 17);
            Language js = new Language("JavaScript", Oopl, new DateTime(1995, 12, 4), 12.0);

            // Ide creation
            Ide vscode = new Ide("VSCode", "Microsoft", new DateTime(2015, 4, 29), 5, null);
            Ide clion = new Ide("CLion", "JetBrains", new DateTime(2015, 4, 14), 1, 71.50);
            Ide intellij = new Ide("IntelliJ", "JetBrains", new DateTime(2019, 12, 12), 2, 300.25);
            Ide pycharm = new Ide("PyCharm", "JetBrains", new DateTime(2010, 2, 3), 2, 119.99);
            Ide rider = new Ide("Rider", "JetBrains", new DateTime(2017, 2, 4), 1, 83.59);
            
            //Software create
            
            // IdeLanguage creation (linking class between Ide and Language with a popularity order for ordering languages inside Ide)
            IdeLanguage javaVscode = new IdeLanguage(vscode, java, 4);
            IdeLanguage csharpVscode = new IdeLanguage(vscode, csharp, 3);
            IdeLanguage pythonVscode = new IdeLanguage(vscode, python, 1);
            IdeLanguage cVscode = new IdeLanguage(vscode, c, 2);
            IdeLanguage jsVscode = new IdeLanguage(vscode, js, 5);
            IdeLanguage javaIntellij = new IdeLanguage(intellij, java, 1);
            IdeLanguage jsIntellij = new IdeLanguage(intellij, js, 2);
            IdeLanguage cClion = new IdeLanguage(clion, c, 1);
            IdeLanguage pythonPycharm = new IdeLanguage(pycharm, python, 1);
            IdeLanguage jsPycharm = new IdeLanguage(pycharm, js, 2);
            IdeLanguage csharpRider = new IdeLanguage(rider, csharp, 1);

            Software spotify = new Software("Spotify", "Music streaming", java);
            Software netflix = new Software("Netflix", "Video streaming", java);
            Software instagram = new Software("Instagram", "Social media", python);
            Software osu = new Software("Osu", "Game", csharp);
            Software doom = new Software("Doom", "Game", c);
            

            
            java.Ides = new List<IdeLanguage> {javaVscode, javaIntellij};
            csharp.Ides = new List<IdeLanguage> {csharpVscode, csharpRider};
            python.Ides = new List<IdeLanguage> {pythonVscode, pythonVscode};
            c.Ides = new List<IdeLanguage> {cVscode, cClion};
            js.Ides = new List<IdeLanguage> {jsVscode, jsIntellij, jsPycharm};

            vscode.Languages = new List<IdeLanguage> {javaVscode, csharpVscode, pythonVscode, cVscode, jsVscode};
            clion.Languages = new List<IdeLanguage> {cClion};
            intellij.Languages = new List<IdeLanguage> {javaIntellij, jsIntellij};
            pycharm.Languages = new List<IdeLanguage> {pythonPycharm, jsPycharm};
            rider.Languages = new List<IdeLanguage> {csharpRider};

            List<IdeLanguage> ideLanguages = new()
            {
                javaVscode, csharpVscode, pythonVscode, cVscode, jsVscode,
                javaIntellij, jsIntellij,
                cClion,
                pythonPycharm, jsPycharm,
                csharpRider
            };
            List<Software> software = new() {spotify, netflix, instagram, osu, doom};
            List<Language> languages = new() {java, csharp, python, c, js};
            List<Ide> ides = new() {vscode, clion, intellij, pycharm, rider};

            
            context.Languages.AddRange(languages);
            context.Ides.AddRange(ides);
            context.Softwares.AddRange(software);
            context.IdeLanguages.AddRange(ideLanguages);


            context.SaveChanges();
            context.ChangeTracker.Clear();
        }
    }
}