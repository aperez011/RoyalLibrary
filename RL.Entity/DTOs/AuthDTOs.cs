using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Entity.DTOs
{
    public class AuthRequest
    {
        public AuthRequest() { }

        public string UserName { get; set; } = default;
        public string Password { get; set; } = default;
    }

    public class AuthResponse
    {
        public AuthResponse() { }

        public int Id { get; set; } = default;
        public string DisplayName { get; set; } = default;
        public string UserName { get; set; } = default;
        public string Email { get; set; } = default;
        public string Role { get; set; } = default;
        public string Token { get; set; } = default;
    }
}
