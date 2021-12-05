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
            IdeLanguage java_vscode = new IdeLanguage(vscode, java, 4);
            IdeLanguage csharp_vscode = new IdeLanguage(vscode, csharp, 3);
            IdeLanguage python_vscode = new IdeLanguage(vscode, python, 1);
            IdeLanguage c_vscode = new IdeLanguage(vscode, c, 2);
            IdeLanguage js_vscode = new IdeLanguage(vscode, js, 5);
            IdeLanguage java_intellij = new IdeLanguage(intellij, java, 1);
            IdeLanguage js_intellij = new IdeLanguage(intellij, js, 2);
            IdeLanguage c_clion = new IdeLanguage(clion, c, 1);
            IdeLanguage python_pycharm = new IdeLanguage(pycharm, python, 1);
            IdeLanguage js_pycharm = new IdeLanguage(pycharm, js, 2);
            IdeLanguage csharp_rider = new IdeLanguage(rider, csharp, 1);

            Software spotify = new Software("Spotify", "Music streaming", java);
            Software netflix = new Software("Netflix", "Video streaming", java);
            Software instagram = new Software("Instagram", "Social media", python);
            Software osu = new Software("Osu", "Game", csharp);
            Software doom = new Software("Doom", "Game", c);
            

            
            java.Ides = new List<IdeLanguage> {java_vscode, java_intellij};
            csharp.Ides = new List<IdeLanguage> {csharp_vscode, csharp_rider};
            python.Ides = new List<IdeLanguage> {python_vscode, python_vscode};
            c.Ides = new List<IdeLanguage> {c_vscode, c_clion};
            js.Ides = new List<IdeLanguage> {js_vscode, js_intellij, js_pycharm};

            vscode.Languages = new List<IdeLanguage> {java_vscode, csharp_vscode, python_vscode, c_vscode, js_vscode};
            clion.Languages = new List<IdeLanguage> {c_clion};
            intellij.Languages = new List<IdeLanguage> {java_intellij, js_intellij};
            pycharm.Languages = new List<IdeLanguage> {python_pycharm, js_pycharm};
            rider.Languages = new List<IdeLanguage> {csharp_rider};

            List<IdeLanguage> ideLanguages = new()
            {
                java_vscode, csharp_vscode, python_vscode, c_vscode, js_vscode,
                java_intellij, js_intellij,
                c_clion,
                python_pycharm, js_pycharm,
                csharp_rider
            };
            List<Software> softwares = new() {spotify, netflix, instagram, osu, doom};
            List<Language> languages = new() {java, csharp, python, c, js};
            List<Ide> ides = new() {vscode, clion, intellij, pycharm, rider};

            
            context.IdeLanguages.AddRange(ideLanguages);
            context.Languages.AddRange(languages);
            context.Ides.AddRange(ides);
            context.Softwares.AddRange(softwares);

            context.SaveChanges();
            context.ChangeTracker.Clear();
        }
    }
}