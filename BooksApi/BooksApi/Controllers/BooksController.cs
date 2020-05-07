using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _bookService.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var book = await _bookService.GetAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Book book)
        {
            await _bookService.CreateAsync(book);
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(string id, Book bookIn)
        {
            var book = _bookService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookService.UpdateAsync(id, bookIn);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var book = await _bookService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            await _bookService.RemoveAsync(id);
            return Ok();
        }
    }
}