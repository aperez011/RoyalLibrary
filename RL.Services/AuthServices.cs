using RL.DataAccess;
using RL.Entity.DTOs;
using RL.Utility;
using RL.Utility.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly dbContextRoyalLibrary _ctx;

        public AuthServices(dbContextRoyalLibrary ctx)
        {
            _ctx = ctx;
        }
        public Task<OperationResult<AuthResponse>> Login(AuthRequest l)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<AuthResponse>> LogOut(int sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
