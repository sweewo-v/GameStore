using FluentValidation;

namespace GameStore.Application.Commands.Game.UpdateGame
{
    public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
    {
        public UpdateGameCommandValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(128).NotEmpty();
            RuleFor(x => x.Description).MinimumLength(10).MaximumLength(4096).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}
