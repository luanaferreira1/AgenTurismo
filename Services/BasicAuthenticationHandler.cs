using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text;

namespace AgenTurismo.Services
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //  *** Credenciais do login ***
            const string validUsername = "admin";
            const string validPassword = "senha123";

            var authHeader = await Task.Run(() => Request.Headers["Authorization"].ToString());

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
            {
                return AuthenticateResult.NoResult();
            }

            return await Task.Run(() =>
            {
                var encodedCredentials = authHeader["Basic ".Length..].Trim();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var credentials = decodedCredentials.Split(':', 2);

                if (credentials.Length != 2 || credentials[0] != validUsername || credentials[1] != validPassword)
                {
                    return AuthenticateResult.Fail("Credenciais inválidas");
                }

                var claims = new[] {
            new Claim(ClaimTypes.Name, validUsername),
            new Claim(ClaimTypes.Role, "Administrator")
        };

                var identity = new ClaimsIdentity(claims, "BasicAuthentication");
                var principal = new ClaimsPrincipal(identity);

                return AuthenticateResult.Success(new AuthenticationTicket(principal, "Cookies"));
            });
        }
    }
}