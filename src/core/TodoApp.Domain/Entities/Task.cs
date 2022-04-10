using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Domain.Entities
{
    [Table("Tasks", Schema = "dbo")]
    public class Task : BaseEntity
    {
        public int TodoId { get; set; }
        [Required, StringLength(100)]
        public string? Name { get; set; }
        public bool IsCompleted { get; set; }
        
        [ForeignKey(nameof(TodoId))]
        [InverseProperty(nameof(Todo.Tasks))]
        public Todo? TodoNavigation { get; set; }
    }
}
