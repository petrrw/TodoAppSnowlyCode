using FluentValidation;
using TodoAppSnowlyCode.Business.Interfaces;
using TodoAppSnowlyCode.Business.Validations;
using TodoAppSnowlyCode.Data.Interfaces;
using TodoAppSnowlyCode.Data.Models;

namespace TodoAppSnowlyCode.Business.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository _repository;
        private readonly ToDoItemValidator _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoItemService"/> class.
        /// </summary>
        /// <param name="repository">Repository providing data access for working with ToDo items.</param>
        /// <param name="validator">ToDo items validator.</param>
        public ToDoItemService(IToDoItemRepository repository, ToDoItemValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        /// <inheritdoc />
        public async Task<IList<ToDoItem>> GetAllAsync(CancellationToken ct) => await _repository.GetAllAsync(ct);

        /// <inheritdoc />
        public async Task<ToDoItem?> GetByIdAsync(int id, CancellationToken ct) => await _repository.GetByIdAsync(id, ct);

        /// <inheritdoc />
        public async Task<ToDoItem> AddAsync(ToDoItem todoItem, CancellationToken ct)
        {
            var validationRes = _validator.Validate(todoItem);

            // Instead of throwing an exception like this, it's better to return a validation result, for example using ErrorOr library or custom result type,
            // so the caller can handle errors explicitly and it's also probably a little bit faster.. i'll leave it like this for simplicity...
            if (!validationRes.IsValid)
                throw new ValidationException(validationRes.Errors);

            return await _repository.AddAsync(todoItem, ct);
        }

        /// <inheritdoc />
        public async Task<ToDoItem?> UpdateAsync(int id, ToDoItem todoItem, CancellationToken ct)
        {
            var validationRes = _validator.Validate(todoItem);

            // Instead of throwing an exception, it's better to return a validation result, for example using ErrorOr library or custom result type,
            // so the caller can handle errors explicitly and it's also propably a little bit faster, but for now, i'll leave it like this for simplicity...
            if (!validationRes.IsValid)
                throw new ValidationException(validationRes.Errors);

            var itemToBeUpdated = await _repository.GetByIdAsync(id, ct);

            if (itemToBeUpdated is null)
                return null;

            todoItem.Id = id;
            todoItem.CreatedAt = itemToBeUpdated.CreatedAt;

            var updatedItem = await _repository.UpdateAsync(todoItem, ct);
            return updatedItem;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id, CancellationToken ct)
        {
            // Fetching whole item from DB just to check if record exists is not optimal..
            if (await _repository.GetByIdAsync(id, ct) is null)
                return false;

            await _repository.RemoveAsync(id, ct);

            return true;
        }
    }
}
