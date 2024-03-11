using RL.DataAccess;
using RL.Entity;
using RL.Entity.DTOs;
using RL.Utility;
using RL.Utility.IServices;
using System.Linq.Expressions;

namespace RL.Services
{
    public class BooksServices : IBookServices
    {
        private readonly dbContextRoyalLibrary _ctx;

        public BooksServices(dbContextRoyalLibrary ctx)
        {
            _ctx = ctx;
        }

        public Task<OperationResult<HashSet<BookResponse>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<HashSet<BookResponse>>> GetByAsync(Expression<Func<Book, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<BookResponse>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<int>> PostAsync(BookRequest b)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> PutAsync(BookUpdateRequest b)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
