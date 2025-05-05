using LibraryManagementSystem.Models.Domain;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ActionResult<List<Book>>> GetAll()=> await _bookRepository.GetAllAsync();
        public async Task<Book?> GetById(Guid id) => await _bookRepository.GetByIdAsync(id);

        public async Task Add(Book book) => await _bookRepository.AddAsync(book);

        public async Task<bool> Update(Book updatedBook)
        {
            var existing = await _bookRepository.GetByIdAsync(updatedBook.Id);
            if (existing == null) return false;

            existing.Title = updatedBook.Title;
            existing.Author = updatedBook.Author;
            existing.Quantity = updatedBook.Quantity;
            existing.Available = updatedBook.Quantity;

            await _bookRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> Lend(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null || book.Available <= 0) return false;
            book.Available--;
            await _bookRepository.UpdateAsync(book);
            return true;
        }

        public async Task<bool> Return(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null || book.Available >= book.Quantity) return false;
            book.Available++;
            await _bookRepository.UpdateAsync(book);
            return true;
        }

        public async Task Delete(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
                await _bookRepository.DeleteAsync(book);
        }

        public Task<List<Book>> Search(string? title, string? author, bool? inStock)
        {
            return _bookRepository.SearchAsync(title, author, inStock);
        }

    }
}
