using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BooksApi
{
    public interface IBookService
    {
        Task<List<Book>> GetAsync();
        Task<Book> GetAsync(string id);
        Task<Book> CreateAsync(Book book);
        Task UpdateAsync(string id, Book book);
        Task RemoveAsync(string id);
    }

    public class BookService : IBookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public async Task<List<Book>> GetAsync() => await _books.FindAsync(book => true).GetAwaiter().GetResult().ToListAsync();

        public async Task<Book> GetAsync(string id) => await _books.FindAsync(book => book.Id == id).GetAwaiter().GetResult().FirstOrDefaultAsync();

        public async Task<Book> CreateAsync(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }

        public async Task UpdateAsync(string id, Book book) => await _books.ReplaceOneAsync(book => book.Id == id, book);

        public async Task RemoveAsync(string id) => await _books.DeleteOneAsync(book => book.Id == id);
    }
}