using RL.Entity.DTOs;
using RL.Entity;

namespace RL.Utility.IServices
{
    public interface IUserServices : IBaseMethods<User, UserResponse>
    {
        Task<OperationResult<int>> PostAsync(UserRequest u);
        Task<OperationResult> PutAsync(UserUpdateRequest u);
    }
}
