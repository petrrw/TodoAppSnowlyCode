namespace TodoAppSnowlyCode.DTO
{
    /// <summary>
    /// DTO for creating a new ToDoItem.
    /// </summary>
    public record CreateToDoItemDto
    {
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
