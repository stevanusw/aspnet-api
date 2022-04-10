using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Domain.Entities
{
    [Table("TodoList", Schema = "dbo")]
    public class Todo : BaseEntity
    {
        [Required, StringLength(50)]
        public string? Name { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
