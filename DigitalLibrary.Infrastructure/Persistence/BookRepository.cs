
using DigitalLibrary.Application.Common.Interfaces.Persistence;
using DigitalLibrary.Application.Common.Interfaces.Utilities;
using DigitalLibrary.Domain;
using DigitalLibrary.Infrastructure.Utilities;

namespace DigitalLibrary.Infrastructure
{
    public class BookRepository : IBookRepository
    {
        private readonly IFileManagement _fileManagement;
        public BookRepository(IFileManagement fileManagement)
        {
            _fileManagement = fileManagement;   
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            string [] files = _fileManagement.GetAllFiles("../Resources/Books");
            
            //Get Books from file name
            List<Book> books = new List<Book>();
            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string[] tokenizedFileName = fileName.Split('-');
                Book book = new Book()
                {
                    Id = tokenizedFileName[0].Trim(),
                    Name = tokenizedFileName[1].Trim(),
                };
                books.Add(book);
            }
            return books;
        }

        public async Task<Book> AddBook(string name, MemoryStream ms)
        {
            int CurrentId = _fileManagement.GetMaxId("../Resources/Books") + 1; 
            string fileName = $"{CurrentId} - {name}"; 
            _fileManagement.AddFile($"../Resources/Books/{fileName}", ms);
            return new Book { Id = CurrentId.ToString(), Name = name};
        }

        public async Task DeleteBook(string id)
        {
            string [] files = _fileManagement.GetAllFiles("../Resources/Books");
            bool successfulDeletion = _fileManagement.DeleteFile(id, files);
            if (!successfulDeletion)
                throw new KeyNotFoundException($"No book with id {id} exists.");
        }

        public async Task<Book> GetBookById(string id)
        {
            (string, string) bookDetails = _fileManagement.GetFileById(id, "../Resources/Books");
            Book book = new Book()
            {
                Id = id,
                Name = bookDetails.Item1,
                Content = bookDetails.Item2
            };
            return book;  
        }
    }
}