using FluentValidation;
using GameStore.Application.ViewModels.Auth;

namespace GameStore.Application.Commands.Auth.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginViewModel>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(e => e.Username)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(3);
            RuleFor(e => e.Password)
                .NotEmpty()
                .MaximumLength(30)
                .MinimumLength(3);
        }
    }
}
