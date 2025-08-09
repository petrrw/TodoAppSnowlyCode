using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoAppSnowlyCode.Data.Models
{
    /// <summary>
    /// Represents a simple ToDo item
    /// </summary>
    public class ToDoItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Short title describing the item.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Determines whether the item is completed.
        /// </summary>
        [Required]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Date and time when the task was created.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The optinal date and time by which the task should be completed
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}
