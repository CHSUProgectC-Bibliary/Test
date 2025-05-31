using BookReviewAPI.Data.Dto;
using BookReviewAPI.Services;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
namespace BookReviewAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("All")]
        public async Task<ActionResult> GetAllBooks(CancellationToken cancellationToken)
        {
            var allBooks = await _bookService.GetAllBooks(cancellationToken);
            return Ok(allBooks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBookById(id, cancellationToken);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpGet("ByCategory/{section}")]
        public async Task<IActionResult> GetBooksByCategory(string section, CancellationToken cancellationToken)
        {
            // Используем UrlDecode, чтобы + → пробел
            var decodedSection = HttpUtility.UrlDecode(section);

            Console.WriteLine($"[API] Категория после декодинга: '{decodedSection}'");

            var books = await _bookService.GetBooksByCategory(decodedSection, cancellationToken);

            if (books == null || !books.Any())
            {
                return NotFound($"Книги в категории '{decodedSection}' не найдены");
            }

            return Ok(books);
        }


        [HttpPost]
        public async Task<IActionResult> AddBook(CreateBookDto bookDto, CancellationToken cancellationToken)
        {
            var book = await _bookService.CreateBook(bookDto, cancellationToken);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Book_Id }, book);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto bookDto, CancellationToken cancellationToken)
        {
            try
            {
                await _bookService.UpdateBook(id, bookDto, cancellationToken);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _bookService.DeleteBook(id, cancellationToken);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
