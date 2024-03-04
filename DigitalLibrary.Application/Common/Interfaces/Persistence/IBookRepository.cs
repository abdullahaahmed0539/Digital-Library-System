
using DigitalLibrary.Domain;

namespace DigitalLibrary.Application.Common.Interfaces.Persistence
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();

        public Task<Book> GetBookById(string id);

        public Task<Book> AddBook(string name, MemoryStream ms);

        public Task DeleteBook(string id);

    }
}