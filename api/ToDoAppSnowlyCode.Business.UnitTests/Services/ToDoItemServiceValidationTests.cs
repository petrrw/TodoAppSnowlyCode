using FluentValidation;
using FluentValidation.Results;
using Moq;
using TodoAppSnowlyCode.Business.Services;
using TodoAppSnowlyCode.Data.Interfaces;
using TodoAppSnowlyCode.Data.Models;

namespace ToDoAppSnowlyCode.Business.UnitTests.Services
{
    internal class ToDoItemServiceValidationTests
    {
        [Test]
        public async Task ValidateToDoItemTest_ValidationSuccess()
        {
            // Arrange
            var todoItemRepositoryMock = new Mock<IToDoItemRepository>();
            var todoItemValidatorMock = new Mock<IValidator<ToDoItem>>();
            todoItemValidatorMock
                .Setup(v => v.Validate(It.IsAny<ToDoItem>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            var todoItemService = new ToDoItemService(todoItemRepositoryMock.Object, todoItemValidatorMock.Object);
            var item = new ToDoItem() { Title = "Title123", Id = 0, CreatedAt = DateTime.UtcNow, IsCompleted = false, DueDate = null };

            // Act
            await todoItemService.AddAsync(item, new CancellationToken());

            // Assert

            // AddAsync should be called once because validation was successful
            todoItemRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void ValidateToDoItemTest_ValidationFail()
        {
            // Arrange
            var todoItemRepositoryMock = new Mock<IToDoItemRepository>();
            var todoItemValidatorMock = new Mock<IValidator<ToDoItem>>();
            todoItemValidatorMock
                .Setup(v => v.Validate(It.IsAny<ToDoItem>()))
                .Returns(new ValidationResult([new ValidationFailure("Title", "Title is too short")]));

            var todoItemService = new ToDoItemService(todoItemRepositoryMock.Object, todoItemValidatorMock.Object);
            var item = new ToDoItem() { Title = "T", Id = 0, CreatedAt = DateTime.UtcNow, IsCompleted = false, DueDate = null };

            // Act
            var exception = Assert.Throws<ValidationException>(() => todoItemService.AddAsync(item, new CancellationToken()).GetAwaiter().GetResult());

            // Assert

            // Service should throw exception with validation error message because validation was not successful
            Assert.That(exception.Message, Is.EqualTo("Validation failed: \r\n -- Title: Title is too short Severity: Error"));

            // AddAsync should not be called because validation wasn't successful
            todoItemRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
