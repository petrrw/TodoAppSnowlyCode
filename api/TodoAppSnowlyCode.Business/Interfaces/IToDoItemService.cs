using TodoAppSnowlyCode.Data.Models;

namespace TodoAppSnowlyCode.Business.Interfaces
{
    /// <summary>
    /// Service layer responsible for handling business logic related to ToDo items.
    /// </summary>
    public interface IToDoItemService
    {
        /// <summary>
        /// Retrieves all stored ToDo items.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>List of ToDo items.</returns>
        Task<IList<ToDoItem>> GetAllAsync(CancellationToken ct);

        /// <summary>
        /// Retrieves a ToDo item by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the ToDo item.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The ToDo item if found, otherwise, null.</returns>
        Task<ToDoItem?> GetByIdAsync(int id, CancellationToken ct);

        /// <summary>
        /// Adds a new ToDo item to the data store.
        /// </summary>
        /// <param name="todoItem">The ToDo item to add.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The added ToDo item.</returns>
        Task<ToDoItem> AddAsync(ToDoItem todoItem, CancellationToken ct);

        /// <summary>
        /// Updates an existing ToDo item.
        /// </summary>
        /// <param name="id">The ID of the ToDo item to update.</param>
        /// <param name="todoItem">The updated ToDo item data.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The updated ToDo item if found, otherwise null.</returns>
        Task<ToDoItem?> UpdateAsync(int id, ToDoItem todoItem, CancellationToken ct);

        /// <summary>
        /// Deletes a ToDo item by its unique identifier.
        /// </summary>
        /// <param name="todoItem">The ID of the ToDo item to delete.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        Task<bool> DeleteAsync(int id, CancellationToken ct);
    }
}
