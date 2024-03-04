using DigitalLibrary.Domain;

namespace DigitalLibrary.Application.Common.Interfaces.Services
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAllBooks();

        public Task<Book> GetBookWithWordCount(string id);

        public Task<Dictionary<string, int>> SearchByPrefix(string id, string prefix);


        public Task<Book> AddBook(string name, MemoryStream ms);

        public Task DeleteBook(string id);

    }
}