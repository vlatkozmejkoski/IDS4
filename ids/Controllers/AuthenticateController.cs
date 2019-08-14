using IdentityServer4.Services;
using ids.Requests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ids.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHostingEnvironment _env;
        public AuthenticateController(IIdentityServerInteractionService interaction, IHostingEnvironment env)
        {
            _interaction = interaction;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var context = _interaction.GetAuthorizationContextAsync(request.ReturnUrl);
            var user = 
        }

    }
}
