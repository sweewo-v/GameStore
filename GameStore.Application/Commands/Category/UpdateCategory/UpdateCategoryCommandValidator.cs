using FluentValidation;

namespace GameStore.Application.Commands.Category.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(128).NotEmpty();
        }
    }
}
