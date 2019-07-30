namespace GameStore.Application.Interfaces.Auth
{
    public interface ISecurityPasswordService
    {
        string SecurityPassword { get; }

        bool IsSecurityPasswordCorrect(string securityPassword);
    }
}
