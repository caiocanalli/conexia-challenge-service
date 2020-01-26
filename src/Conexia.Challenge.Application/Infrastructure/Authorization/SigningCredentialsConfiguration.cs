using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Conexia.Challenge.Application.Infrastructure.Authorization
{
    public class SigningCredentialsConfiguration
    {
        private const string SecretKey = "520cef23-da17-4e53-96a5-5b2b178a61ac";
        public static readonly SymmetricSecurityKey Key =
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
