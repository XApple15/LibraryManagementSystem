using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models.Domain;
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

      

        public async Task<Book?> GetByIdAsync(Guid id) => await _libraryContext.Books.FindAsync(id);

        public async Task AddAsync(Book book)
        {
            book.Available = book.Quantity;
            _libraryContext.Books.Add(book);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _libraryContext.Books.Update(book);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _libraryContext.Books.Remove(book);
            await _libraryContext.SaveChangesAsync();
        }
        public async Task<List<Book>> SearchAsync(string? title, string? author, bool? inStock)
        {
            var query = _libraryContext.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.Author.Contains(author));

            if (inStock.HasValue)
                query = inStock.Value
                    ? query.Where(b => b.Available > 0)
                    : query.Where(b => b.Available == 0);

            return await query.ToListAsync();
        }
    }
}
