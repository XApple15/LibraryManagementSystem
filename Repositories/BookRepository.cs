using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _libraryContext;
        public BookRepository(LibraryDbContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<List<Book>> GetAllAsync() => await _libraryContext.Books.ToListAsync();

        public async Task AddAsync(Book book)
        {

        }
    }
}
