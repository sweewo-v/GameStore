﻿using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using GameStore.Application.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand>
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IPasswordValidator<IdentityUser> passwordValidator;

        public CreateAdminCommandHandler(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, IPasswordValidator<IdentityUser> passwordValidator)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.passwordValidator = passwordValidator;
        }

        public async Task<Unit> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            if (roleManager.IsAdminAvailable())
            {
                return Unit.Value;
            }

            var adminRole = new IdentityRole("Admin");
            var admin = new IdentityUser(request.Username);

            var res = await passwordValidator.ValidateAsync(userManager, admin, request.Password);
            if (res.Errors.Any())
            {
                throw new AuthenticationException(string.Join(" ",
                    res.Errors.Select(e => e.Description)));
            }

            await userManager.CreateAsync(admin, request.Password);
            await roleManager.CreateAsync(adminRole);
            await userManager.AddToRoleAsync(admin, adminRole.Name);

            return Unit.Value;
        }
    }
}
