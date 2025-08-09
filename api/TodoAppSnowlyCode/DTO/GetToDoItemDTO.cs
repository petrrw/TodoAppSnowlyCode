namespace TodoAppSnowlyCode.DTO
{
    /// <summary>
    /// DTO representing a ToDo item returned from the API.
    /// </summary>
    public record GetToDoItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
