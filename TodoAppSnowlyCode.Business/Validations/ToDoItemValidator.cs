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

            // It could be nice to check whether the DueDate is in future/>CreatedAt, but I'll leave it like this so creating new records from swagger is not so annoying..
        }
    }
}
