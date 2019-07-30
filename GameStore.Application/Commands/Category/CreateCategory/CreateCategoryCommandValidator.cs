using FluentValidation;

namespace GameStore.Application.Commands.Category.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(128).NotEmpty();
        }
    }
}
