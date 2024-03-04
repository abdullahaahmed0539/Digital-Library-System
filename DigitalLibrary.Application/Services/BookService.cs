

using DigitalLibrary.Application.Common.Interfaces.Persistence;
using DigitalLibrary.Application.Common.Interfaces.Services;
using DigitalLibrary.Application.Common.Utilities;
using DigitalLibrary.Domain;
using Microsoft.Extensions.Caching.Memory;


namespace DigitalLibrary.Application.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IMemoryCache _memoryCache;


        public BookService(IBookRepository bookRepository,  IMemoryCache memoryCache)
        {
            _bookRepository = bookRepository;
            _memoryCache = memoryCache; 

        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAllBooks();
        }

        public async Task<Book> AddBook(string name, MemoryStream ms)
        {
            name = name.ToUpper().Trim();
            return await _bookRepository.AddBook(name, ms);
        }

        public async Task DeleteBook(string id)
        {
            await _bookRepository.DeleteBook(id);
        }

        public async Task<Dictionary<string, int>> SearchByPrefix(string id, string prefix)
        {

            //check if the book is already in cache
            if (!_memoryCache.TryGetValue(id, out Book book))
            {

                //load book details
                book = await GetBookDetails(id);

                // Cache the book content with a expiration
                _memoryCache.Set(id, book, TimeSpan.FromMinutes(5));
            }

            //get top 10 words
            Dictionary<string, int>? matchingWords = book.WordsCount?
                .Where(pair => pair.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .Take(10)
                .ToDictionary(pair => pair.Key.ToUpper(), pair => pair.Value);

            return matchingWords!;
        }


        public async Task<Book> GetBookWithWordCount(string id)
        {
            if (!_memoryCache.TryGetValue(id, out Book book))
            {
                //load book details
                book = await GetBookDetails(id);

                // Cache the book content with a expiration
                _memoryCache.Set(id, book, TimeSpan.FromMinutes(5));
            }

            //add to cache
            _memoryCache.Set(id, book, TimeSpan.FromMinutes(5));

            //get top 10 words
            Dictionary<string, int>? topTenWords = book.WordsCount?
            .Take(10)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

            Book bookWithTopTenWords = new Book()
            {
                Id = book.Id,
                Name = book.Name,
                WordsCount = topTenWords
            };

            return bookWithTopTenWords;
        }

        private async Task<Book> GetBookDetails(string id)
        {
            Book book = await _bookRepository.GetBookById(id);

            //remove characters
            book.Content = StringFormatter.RemovePunctuationAndNumbers(book.Content);

            //tokenize, remove less than 5, and desc
            book.WordsCount = book.Content
                .Split(' ')
                .Where(x => x.Length >= 5)
                .GroupBy(x => x)
                .Select(x => new 
                { 
                    KeyField = x.Key, 
                    Count = x.Count() 
                })
                .OrderByDescending(x => x.Count)
                .ToDictionary(x => x.KeyField, x => x.Count);

            return book; 
        }
    }
}