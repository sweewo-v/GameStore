using FluentValidation;

namespace GameStore.Application.Commands.Order.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(128).NotEmpty();
            RuleFor(x => x.City).MinimumLength(3).MaximumLength(128).NotEmpty();
            RuleFor(x => x.Country).MinimumLength(3).MaximumLength(128).NotEmpty();
            RuleFor(x => x.Line1).MinimumLength(3).MaximumLength(128).NotEmpty();
            RuleFor(x => x.Line2).MaximumLength(128);
            RuleFor(x => x.Line3).MaximumLength(128);
            RuleFor(x => x.State).MinimumLength(2).MaximumLength(128).NotEmpty();
            RuleFor(x => x.Zip).MinimumLength(10).MaximumLength(10).NotEmpty();
        }
    }
}
