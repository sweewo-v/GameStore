using GameStore.Application.ViewModels.Auth;
using MediatR;

namespace GameStore.Application.Commands.Auth.LoginUser
{
    public class LoginUserCommand : IRequest
    {
        public LoginViewModel LoginViewModel { get; set; }
    }
}
