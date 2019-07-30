﻿namespace GameStore.Application.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
