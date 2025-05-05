using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
    }
}
