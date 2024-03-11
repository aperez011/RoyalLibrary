using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace RL.Entity
{
    public class User : BaseProperties
    {
        public string FirstName { get; set; } = default;
        public string LastName { get; set; } = default;
        public string UserName { get; set; } = default;
        public string Password { get; set; } = default;
        public string Email { get; set; } = default;
        public string Role { get; set; } = default;
    }
}
