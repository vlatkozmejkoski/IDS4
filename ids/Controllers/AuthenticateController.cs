using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Test;
using ids.Helpers;
using ids.Requests;
using ids.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ids.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticateController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHostingEnvironment _env;
        private readonly TestUserStore _users;
        public AuthenticateController(
            IIdentityServerInteractionService interaction,
            IHostingEnvironment env,
            TestUserStore users = null)
        {
            _users = users ?? new TestUserStore(TestUsers.GetTestUsers());
            _interaction = interaction;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var context = _interaction.GetAuthorizationContextAsync(request.ReturnUrl);
            var isValidUser = _users.ValidateCredentials(request.Username, request.Password);
            if (!isValidUser)
            {
                return Unauthorized();
            }
            var user = _users.FindByUsername(request.Username);

            AuthenticationProperties props = null;
            if (request.RememberLogin)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                };
            }
            var claimsIdentity = new ClaimsIdentity(user.Claims);
            await HttpContext.SignInAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme, new ClaimsPrincipal(claimsIdentity), props);

            return new JsonResult(new { RedirectUrl = request.ReturnUrl, NotOK = false });
        }

    }
}
