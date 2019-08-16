using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ids.Requests
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberLogin { get; set; }
    }
}
