using FluentValidation;
using TodoAppSnowlyCode.Data.Models;

namespace TodoAppSnowlyCode.Business.Validations
{
    public class ToDoItemValidator : AbstractValidator<ToDoItem>
    {
        public ToDoItemValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);

            RuleFor(p => p.IsCompleted)
                .NotNull();

            RuleFor(p => p.CreatedAt)
                .LessThan(p => p.DueDate);
        }
    }
}
