using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Entity.DTOs
{
    public class UserRequest
    {
        public string FirstName { get; set; } = default;
        public string LastName { get; set; } = default;
        public string UserName { get; set; } = default;
        public string Password { get; set; } = default;
        public string Role { get; set; } = default;
    }

    public class UserUpdateRequest : UserRequest
    {
        public int Id { get; set; } = default;
    }

    public class UserResponse
    {
        public int Id { get; set; } = default;
        public string UserName { get; set; } = default;
        public string EmployeeName { get; set; } = default;
        public string Email { get; set; } = default;
        public string Role { get; set; } = default;
    }
}
