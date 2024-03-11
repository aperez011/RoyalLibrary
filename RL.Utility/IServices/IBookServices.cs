using RL.Entity;
using RL.Entity.DTOs;

namespace RL.Utility.IServices
{
    public interface IBookServices : IBaseMethods<Book, BookResponse>
    {
        Task<OperationResult<int>> PostAsync(BookRequest b);
        Task<OperationResult> PutAsync(BookUpdateRequest b);
    }
}
