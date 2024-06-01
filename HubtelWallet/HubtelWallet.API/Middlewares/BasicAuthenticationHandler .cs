using HubtelWallet.Application;
using HubtelWallet.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace HubtelWallet.API.Middlewares
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<BasicAuthenticationHandler> _logger;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceManager serviceManager) : base(options, logger, encoder, clock)
            {
                _serviceManager = serviceManager;
                _logger = logger.CreateLogger<BasicAuthenticationHandler>();
            }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header missing");
            }

            var isAuthenticated = Context.Session.GetString("IsAuthenticated");
            var sessionUsername = Context.Session.GetString("Username");
            if (isAuthenticated == "true" && sessionUsername is not null)
            {
                _logger.LogDebug("User is already authenticated");

                var ticket = CreateTicket(sessionUsername);
                return AuthenticateResult.Success(ticket);
            }

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                var isValid = await _serviceManager.CustomerService.ValidateCustomerToken(username, password);
                if (!isValid)
                {
                    return AuthenticateResult.Fail("Invalid username or password");
                }


                var ticket = CreateTicket(username);
                Context.Session.SetString("Username", username.ToInternationalNumber());
                Context.Session.SetString("IsAuthenticated", "true");

                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid authorization header");
            }
        }

        private AuthenticationTicket CreateTicket(string username)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return ticket;
        }
    }
}
