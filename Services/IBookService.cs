using LibraryManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Services
{
    public interface IBookService
    {
        Task<ActionResult<List<Book>>> GetAll();
        Task<bool> Lend(Guid id);
        Task<bool> Return(Guid id);
        Task<Book?> GetById(Guid id);
        Task Add(Book book);
        Task Delete(Guid id);
        Task<bool> Update(Book updatedBook);
        Task<List<Book>> Search(string? title, string? author, bool? inStock);
    }
}
