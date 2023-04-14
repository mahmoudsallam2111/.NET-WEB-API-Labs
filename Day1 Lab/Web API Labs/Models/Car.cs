using Web_API_Labs.Validations;

namespace Web_API_Labs.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Datevalidation]  // is a custom validation  
        public DateTime ProductionDate { get; set; }

        public string Type { get; set; }= string.Empty;

    }
}
