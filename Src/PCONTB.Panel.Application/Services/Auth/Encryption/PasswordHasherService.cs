using PCONTB.Panel.Application.Contracts.Application.Services.Auth.Encryption;

namespace PCONTB.Panel.Infrastructure.Security.Auth.Encryption
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public string Generate(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool Verify(string providePassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(providePassword, password);
        }
    }
}
