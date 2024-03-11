using RL.DataAccess;
using RL.Entity;
using RL.Entity.DTOs;
using RL.Utility;
using RL.Utility.IServices;
using System.Linq.Expressions;

namespace RL.Services
{
    public class UserServices : IUserServices
    {
        private readonly dbContextRoyalLibrary _ctx;
        public UserServices(dbContextRoyalLibrary ctx)
        {
            _ctx = ctx;
        }

        public Task<OperationResult<HashSet<UserResponse>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<HashSet<UserResponse>>> GetByAsync(Expression<Func<User, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<UserResponse>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<int>> PostAsync(UserRequest u)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> PutAsync(UserUpdateRequest u)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
