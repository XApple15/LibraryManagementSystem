using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Services
{
    public interface IBookService
    {
        Task<ActionResult<List<Book>>> GetAll();
    }
}
