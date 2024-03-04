using Microsoft.AspNetCore.Mvc;
using DigitalLibrary.Application.Common.Interfaces.Services;
using DigitalLibrary.WebApi.DTOs.Responses;
using DigitalLibrary.WebApi.DTOs.Requests;
using DigitalLibrary.Domain;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using DigitalLibrary.Infrastructure.Utilities;

namespace DigitalLibrary.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
  
    private readonly IBookService _bookService;


    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        GetAllBooksResponse response = new GetAllBooksResponse();
        IEnumerable<Book> books = await _bookService.GetAllBooks();
        response.books = new List<bookDetail>();
        if (books.Count() > 0)
        {
            foreach(Book book in books)
            {
                response.books.Add(new bookDetail(){Name = book.Name, Id=book.Id});
            }
        }
        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetTopTenWord(string id)
    {
        Book book = await _bookService.GetBookWithWordCount(id);
        GetTopTenWordsResponse response = new GetTopTenWordsResponse()
        {
            BookName = book.Name,
            TopTenWords = book.WordsCount.ToList()
            // TopTenWords = JsonConvert.SerializeObject(book.WordsCount)
        };
        return Ok(response);
    }
    

    [HttpGet("{id}/search")]
    public async Task<IActionResult> SearchWordsByPrefix(string id, [FromQuery] string prefix)
    {
        // Validate input parameters
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(prefix) || prefix.Length < 3)
            throw new AppException("Invalid parameters");

        Dictionary<string, int> topTenMatchedWords = await _bookService.SearchByPrefix(id, prefix);
        MatchedWordsResponse response = new MatchedWordsResponse()
        {
            MatchedWords = topTenMatchedWords.ToList()
        };
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddBookRequest bookToSave)
    {
        if (bookToSave.bookFile?.Length == 0)
            throw new AppException("No file uploaded");

        if(Path.GetExtension(bookToSave.bookFile?.FileName) != ".txt")
            throw new AppException("Incorrect file format uploaded. Please upload .txt file.");
        

        using (var ms = new MemoryStream())
        {
            bookToSave.bookFile?.CopyTo(ms);
            Book bookCreated = await _bookService.AddBook(bookToSave.bookName, ms);
            CreateBookResponse response = new CreateBookResponse() { BookCreated = new bookDetail(){Id = bookCreated.Id, Name = bookCreated.Name}}; 
            return CreatedAtAction(nameof(Create), response);
        }
    }
    

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _bookService.DeleteBook(id);
        return NoContent();
    }

}
