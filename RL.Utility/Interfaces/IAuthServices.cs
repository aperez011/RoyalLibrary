using RL.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Utility.IServices
{
    public interface IAuthServices
    {
        Task<OperationResult<AuthResponse>> Login(AuthRequest l);
        Task<OperationResult<AuthResponse>> LogOut(int sessionId);
    }
}
