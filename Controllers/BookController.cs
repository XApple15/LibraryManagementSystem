using LibraryManagementSystem.Models.Domain;
using LibraryManagementSystem.Models.DTO;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAll() => await _bookService.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(Guid id)
        {
            var book = await _bookService.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookDTO book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookToAdd = new Book
            {
                Title = book.Title,
                Author = book.Author,
                Quantity = book.Quantity,
                Available = book.Quantity
            };
            await _bookService.Add(bookToAdd);
            return CreatedAtAction(nameof(Get), new { id = bookToAdd.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Book updatedBook)
        {
            if (id != updatedBook.Id) return BadRequest("ID mismatch");

            var success = await _bookService.Update(updatedBook);
            return success ? NoContent() : NotFound();
        }

        [HttpPost("{id}/lend")]
        public async Task<IActionResult> Lend(Guid id)
        {
            return await _bookService.Lend(id) ? Ok() : BadRequest("Book not available");
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> Return(Guid id)
        {
            return await _bookService.Return(id) ? Ok() : BadRequest("All copies already returned");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.Delete(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Book>>> Search([FromQuery] string? title, [FromQuery] string? author, [FromQuery] bool? inStock)
        {
            var results = await _bookService.Search(title, author, inStock);
            return Ok(results);
        }

    }
}
