namespace Languages.BL.Domain
{
    public class Program
    {
        public string Name { get; set; }
        public Language Language { get; set; }
        public string Description { get; set; }

        public Program(string name, Language language, string description)
        {
            Name = name;
            Language = language;
            Description = description;
        }
    }
}