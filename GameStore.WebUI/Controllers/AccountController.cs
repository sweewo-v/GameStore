using System.Security.Authentication;
using System.Threading.Tasks;
using GameStore.Application.Commands.Admin.CreateAdmin;
using GameStore.Application.Commands.Auth.LoginUser;
using GameStore.Application.Commands.Auth.LogoutUser;
using GameStore.Application.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebUI.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(CreateAdminCommand createAdminCommand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await Mediator.Send(createAdminCommand);

                    return RedirectToAction("Login", new LoginViewModel
                    {
                        ReturnUrl = createAdminCommand.ReturnUrl
                    });
                }
                catch (AuthenticationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View("Login", new LoginViewModel
            {
                ReturnUrl = createAdminCommand.ReturnUrl,
                Username = createAdminCommand.Username
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await Mediator.Send(new LoginUserCommand
                    {
                        LoginViewModel = loginViewModel
                    });

                    return Redirect(loginViewModel?.ReturnUrl ?? "/admin");
                }
                catch (AuthenticationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(loginViewModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await Mediator.Send(new LogoutUserCommand());

            return Redirect(returnUrl);
        }
    }
}