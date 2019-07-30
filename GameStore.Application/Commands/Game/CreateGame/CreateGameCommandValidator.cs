using FluentValidation;

namespace GameStore.Application.Commands.Game.CreateGame
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(128).NotEmpty();
            RuleFor(x => x.Description).MinimumLength(10).MaximumLength(4096).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}
