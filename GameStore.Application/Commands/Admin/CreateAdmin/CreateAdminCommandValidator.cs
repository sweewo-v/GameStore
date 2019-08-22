using FluentValidation;

namespace GameStore.Application.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
    {
        public CreateAdminCommandValidator()
        {
            RuleFor(e => e.Username)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(3);
            RuleFor(e => e.Password)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(3);
        }
    }
}
