namespace TodoAppSnowlyCode.DTO
{
    /// <summary>
    /// DTO for updating a ToDo item.
    /// </summary>
    public record UpdateToDoItemDto
    {
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
