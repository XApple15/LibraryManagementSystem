using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<List<Book>> SearchAsync(string? title, string? author, bool? inStock);
    }
}
