using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class BookRepository : IBookRepository
    {
        private DataContext _db;

        public BookRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<Book?> GetBook(int id)
        {
            return await _db.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(book => book.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<Book?> UpdateBook(Book book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
            return await _db.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == book.Id);
        }

        public async Task<bool> DeleteBook(int id)
        {
            var foundBook = await _db.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (foundBook == null)
            {
                return false;
            }
            _db.Books.Remove(foundBook);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task CreateBook(Book book)
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
        }
    }
}