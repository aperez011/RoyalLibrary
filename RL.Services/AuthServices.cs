using RL.DataAccess;
using RL.Entity;
using RL.Entity.DTOs;
using RL.Share;
using RL.Utility;
using RL.Utility.Interfaces;
using RL.Utility.IServices;

namespace RL.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly dbContextRoyalLibrary _ctx;
        private readonly IJwtProvider _jwtProvider;

        public AuthServices(dbContextRoyalLibrary ctx, IJwtProvider jwtProvider)
        {
            _ctx = ctx;
            _jwtProvider = jwtProvider;
        }

        public async Task<OperationResult<AuthResponse>> Login(AuthRequest l)
        {
            try
            {
                var us = await _ctx.GetByAsync<User>(c => c.UserName == l.UserName);
                if (!us.Any())
                    return OperationResult<AuthResponse>.Fail("User name or password don't mach.");

                var _us = us.SingleOrDefault(c => c.Password.DecryptText() == l.Password);
                if (_us is null)
                    return OperationResult<AuthResponse>.Fail("User name or password don't mach.");

                var userInfo = new AuthResponse
                {
                    Id = _us.Id,
                    DisplayName = string.Concat(_us.FirstName, " ", _us.LastName),
                    UserName = _us.UserName,
                    Email = _us.Email,
                    Role = _us.Role
                };

                //Add a session table to check the token


                //Generate token
                userInfo.Token = _jwtProvider.GenerateToken(userInfo);

                return OperationResult<AuthResponse>.Success(userInfo);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                assignEntities = null;
            }

        }

        public async Task<OperationResult<AuthResponse>> LogOut(int sessionId)
        {
            //NOTE: waiting for the session table
            return await Task.FromResult(OperationResult<AuthResponse>.Success());
        }
    }
}
