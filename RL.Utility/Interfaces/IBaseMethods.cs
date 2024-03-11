using RL.Entity;
using System.Linq.Expressions;

namespace RL.Utility.IServices
{
    public interface IBaseMethods<TEntity, TResponse>
        where TEntity : BaseProperties, new()
        where TResponse : class, new()
    {
        Task<OperationResult<HashSet<TResponse>>> GetAllAsync();
        Task<OperationResult<TResponse>> GetByIdAsync(int id);
        Task<OperationResult<HashSet<TResponse>>> GetByAsync(Expression<Func<TEntity, bool>> condition);
        Task<OperationResult> DeleteAsync(int id);
    }
}
