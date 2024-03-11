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

        public async Task<OperationResult<HashSet<BookResponse>>> GetAllAsync()
        {
            try
            {
                var books = await _ctx.GetAllAsync<Book>();
                var resp = BookToBookResponse(books);

                return OperationResult<HashSet<BookResponse>>.Success(resp);
            }
            catch (Exception ex)
            {
                return OperationResult<HashSet<BookResponse>>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<HashSet<BookResponse>>> GetByAsync(Expression<Func<Book, bool>> condition)
        {
            try
            {
                var books = await _ctx.GetByAsync<Book>(condition);
                var resp = BookToBookResponse(books);

                return OperationResult<HashSet<BookResponse>>.Success(resp);
            }
            catch (Exception ex)
            {
                return OperationResult<HashSet<BookResponse>>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<BookResponse>> GetByIdAsync(int id)
        {
            try
            {
                var books = await _ctx.GetByIdAsync<Book>(id);
                var resp = BookToBookResponse(new HashSet<Book> { books });

                return OperationResult<BookResponse>.Success(resp.Single());
            }
            catch (Exception ex)
            {
                return OperationResult<BookResponse>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult<int>> PostAsync(BookRequest b)
        {
            try
            {
                var book = new Book
                {
                    Title =  b.Title,
                    Publisher = b.Publisher,
                    FirstName =  b.FirstName,
                    LastName =  b.LastName,
                    TotalCopies = b.TotalCopies,
                    CopiesInUse = 0,
                    Type = b.Type,
                    ISBN = b.ISBN,
                    Category = b.Category
                };

                var resp = await _ctx.PostAsync(book);

                return OperationResult<int>.Success(resp.Data);
            }
            catch (Exception ex)
            {
                return OperationResult<int>.Fail(ex.Message);
            }
        }

        public async Task<OperationResult> PutAsync(BookUpdateRequest b)
        {
            try
            {
                var book = new Book
                {
                    Id = b.Id,
                    Title = b.Title,
                    Publisher = b.Publisher,
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    TotalCopies = b.TotalCopies,
                    CopiesInUse = 0,
                    Type = b.Type,
                    ISBN = b.ISBN,
                    Category = b.Category
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
                _ = await _ctx.DeleteAsync<Book>(id);
                return OperationResult<BookResponse>.Success();
            }
            catch (Exception ex)
            {
                return OperationResult<BookResponse>.Fail(ex.Message);
            }
        }

        #region [ Mapping ]
        private HashSet<BookResponse> BookToBookResponse(HashSet<Book> books)
        {
            return books.Select(c =>
                new BookResponse
                {
                    Id = c.Id,
                    Title = c.Title,
                    Publisher = c.Publisher,
                    Author = string.Concat(c.LastName, " ", c.FirstName),
                    Type =  c.Type,
                    ISBN =  c.ISBN,
                    Category =  c.Category,
                    Avalible = $"{c.TotalCopies - c.CopiesInUse}/{c.TotalCopies}",
                }).ToHashSet();
        }
        #endregion [ Mapping]
    }
}
