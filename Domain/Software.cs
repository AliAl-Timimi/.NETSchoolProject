using System.ComponentModel.DataAnnotations;

namespace Languages.BL.Domain
{
    public class Software
    {
        [Key]
        public long id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Software(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}