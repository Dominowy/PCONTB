namespace PCONTB.Panel.Application.Contracts.Services.Auth.Encryption
{
    public interface IPasswordHasherService
    {
        string Generate(string password);
        bool Verify(string providePassword, string password);
    }
}
