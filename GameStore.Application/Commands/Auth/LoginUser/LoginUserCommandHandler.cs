using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.Commands.Auth.LoginUser
{
    public class LoginUserCommandHandler: IRequestHandler<LoginUserCommand>
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public LoginUserCommandHandler(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.LoginViewModel.Username);

            if (user != null)
            {
                await signInManager.SignOutAsync();
                if ((await signInManager.PasswordSignInAsync(user,
                    request.LoginViewModel.Password,
                    true, false)).Succeeded)
                {
                    return Unit.Value;
                }
            }

            throw new AuthenticationException("Invalid username or password.");
        }
    }
}
