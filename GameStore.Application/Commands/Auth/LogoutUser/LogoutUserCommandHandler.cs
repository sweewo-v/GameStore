using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.Commands.Auth.LogoutUser
{
    public class LogoutUserCommandHandler:IRequestHandler<LogoutUserCommand>
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public LogoutUserCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();

            return Unit.Value;
        }
    }
}
