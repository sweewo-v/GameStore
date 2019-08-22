using MediatR;

namespace GameStore.Application.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommand : IRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
