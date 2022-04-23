using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TodoApp.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? MessageTemplate { get; set; }
        public string? Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Exception { get; set; }
        public string? Properties { get; set; }

        [NotMapped]
        public XElement? PropertiesXml => Properties != null ? XElement.Parse(Properties) : null;
    }
}
