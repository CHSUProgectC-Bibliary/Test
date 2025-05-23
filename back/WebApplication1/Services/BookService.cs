using AutoMapper;
using BookReviewAPI.Data;
using BookReviewAPI.Data.Dto;
using BookReviewAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
namespace BookReviewAPI.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooks(CancellationToken cancellationToken);
        Task<BookDto> GetBookById(int id, CancellationToken cancellationToken);
        Task<BookDto> CreateBook(CreateBookDto bookDto, CancellationToken cancellationToken);
        Task UpdateBook(int id, UpdateBookDto bookDto, CancellationToken cancellationToken);
        Task DeleteBook(int id, CancellationToken cancellationToken);
    }
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BookService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BookDto> CreateBook(CreateBookDto bookDto, CancellationToken cancellationToken) 
        {
            var book = _mapper.Map<Book>(bookDto);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return _mapper.Map<BookDto>(book);
        }

        public async Task DeleteBook(int id, CancellationToken cancellationToken) // не имеет смысла, так как кне используется в строках 35-39. Нужен только для отмены операции БД (либо добавлять использование, либо убрать), также ниже смсотри
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) throw new NotFoundException("Book not found");
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks(CancellationToken cancellationToken)
        {
            var allBooks = await _context.Books.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookDto>>(allBooks);
        }

        public async Task<BookDto> GetBookById(int id, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task UpdateBook(int id, UpdateBookDto BookDto, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) throw new NotFoundException("Book not found");

            _mapper.Map(BookDto, book);
            await _context.SaveChangesAsync();
        }
    }
}
