using System;
using GameStore.Application.Interfaces.Auth;

namespace GameStore.Application.Infrastructure.Auth
{
    public class SecurityPasswordService : ISecurityPasswordService
    {
        public string SecurityPassword => _securityPassword.ToString();

        private readonly Guid _securityPassword;

        public SecurityPasswordService()
        {
            _securityPassword = Guid.NewGuid();
        }

        public bool IsSecurityPasswordCorrect(string securityPassword)
        {
            if (Guid.TryParse(securityPassword, out Guid result))
            {
                if (result.Equals(_securityPassword))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
