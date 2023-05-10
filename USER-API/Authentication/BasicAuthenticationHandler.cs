using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using USER_API.Services;

namespace USER_API.Authentication;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserService _userService;
    
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, UrlEncoder encoder, 
        ISystemClock clock, 
        IUserService userService) : 
        base(options, logger, encoder, clock)
    {
        _userService = userService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = Request.Headers["Authorization"].ToString();
        if (authorizationHeader != null && authorizationHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
        {
            var token = authorizationHeader.Substring("Basic ".Length).Trim();
            var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = credentialsAsEncodedString.Split(':');
            string role = await _userService.Authenticate(credentials[0], credentials[1]);
            if (role != "None")
            {
                var claims = new[] { new Claim("login", credentials[0]), new Claim(ClaimTypes.Role, role) };
                var identity = new ClaimsIdentity(claims, "Basic");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
            }
        }
        
        Response.StatusCode = 401;
        Response.Headers.Add("WWW-Authenticate", $"Basic realm=\", charset=\"UTF-8\"");
        return await Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
    }
}