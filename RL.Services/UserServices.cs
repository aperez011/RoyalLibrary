using RL.DataAccess;
using RL.Entity;
using RL.Entity.DTOs;
using RL.Utility;
using RL.Utility.IServices;
using RL.Share;
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

        public async Task<OperationResult<HashSet<UserResponse>>> GetAllAsync()
        {
            try
            {
                var books = await _ctx.GetAllAsync<User>();
                var resp = UserToUserResponse(books);

                return OperationResult<HashSet<UserResponse>>.Success(resp);
            }
            catch (Exception ex)
            {
                return OperationResult<HashSet<UserResponse>>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<HashSet<UserResponse>>> GetByAsync(Expression<Func<User, bool>> condition)
        {
            try
            {
                var books = await _ctx.GetByAsync<User>(condition);
                var resp = UserToUserResponse(books);

                return OperationResult<HashSet<UserResponse>>.Success(resp);
            }
            catch (Exception ex)
            {
                return OperationResult<HashSet<UserResponse>>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<UserResponse>> GetByIdAsync(int id)
        {
            try
            {
                var books = await _ctx.GetByIdAsync<User>(id);
                var resp = UserToUserResponse(new HashSet<User> { books });

                return OperationResult<UserResponse>.Success(resp.Single());
            }
            catch (Exception ex)
            {
                return OperationResult<UserResponse>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<int>> PostAsync(UserRequest u)
        {
            try
            {
                var book = new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Password = u.Password.EncryptText(),
                    Email = u.Email,
                    Role = u.Role
                };

                var resp = await _ctx.PostAsync(book);

                return OperationResult<int>.Success(resp.Data);
            }
            catch (Exception ex)
            {
                return OperationResult<int>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult> PutAsync(UserUpdateRequest u)
        {
            try
            {
                var book = new User
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Password = u.Password.EncryptText(),
                    Email = u.Email,
                    Role = u.Role
                };

                var resp = await _ctx.PustAsync(book);

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                _ = await _ctx.DeleteAsync<User>(id);
                return OperationResult<UserResponse>.Success();
            }
            catch (Exception ex)
            {
                return OperationResult<UserResponse>.Fail(ex.Message);
            }
        }

        #region [ Mapping ]
        private HashSet<UserResponse> UserToUserResponse(HashSet<User> users)
        {
            return users.Select(c =>
                new UserResponse
                {
                    Id = c.Id,
                    EmployeeName = string.Concat(c.FirstName, " ", c.LastName),
                    UserName = c.UserName,
                    Email = c.Email,
                    Role = c.Role
                }).ToHashSet();
        }
        #endregion [ Mapping]
    }
}
