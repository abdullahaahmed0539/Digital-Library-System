using DigitalLibrary.Application.Common.Interfaces.Services;
using DigitalLibrary.Domain;
using DigitalLibrary.Infrastructure.Utilities;
using DigitalLibrary.WebApi.Controllers;
using DigitalLibrary.WebApi.DTOs.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DigitalLibrary.Tests.WebApi
{
    public class BookControllerTest
    {
        [Fact]
        public async Task BookController_Get_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockBookService = new Mock<IBookService>();
            List<Book> mockListOfBooks = new List<Book>() {
                new Book(){Id = "1", Name = "Harry Potter 1"},
                new Book(){Id = "2", Name = "Harry Potter 2"}
             };
            mockBookService.Setup(x => x.GetAllBooks()).ReturnsAsync(mockListOfBooks);
            BooksController booksController = new BooksController(mockBookService.Object);

            //Act
            var result = (OkObjectResult) await booksController.Get();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        
        [Theory]
        [InlineData("1","Wor")]
        [InlineData("2","Worl")]
        public async Task BookController_SearchWordsByPrefix_OnSuccess_ReturnsStatusCode200(string id, string prefix)
        {
            //Arrange
            var mockBookService = new Mock<IBookService>();
            Dictionary<string, int> mockWordCount = new Dictionary<string, int>();
            mockWordCount.Add("World", 3);

            mockBookService.Setup(x => x.SearchByPrefix(id, prefix)).ReturnsAsync(mockWordCount);
            BooksController booksController = new BooksController(mockBookService.Object);

            //Act
            var result = (OkObjectResult) await booksController.SearchWordsByPrefix(id, prefix);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Theory]
        [InlineData("1","Wor")]
        [InlineData("2","Worl")]
        public async Task BookController_SearchWordsByPrefix_OnSuccess_ReturnsMatchedWordsResponse(string id, string prefix)
        {
            //Arrange
            var mockBookService = new Mock<IBookService>();
            Dictionary<string, int> mockWordCount = new Dictionary<string, int>
            {
                { "World", 3 }
            };
            mockBookService.Setup(x => x.SearchByPrefix(id, prefix)).ReturnsAsync(mockWordCount);
            BooksController booksController = new BooksController(mockBookService.Object);

            //Act
            var result = await booksController.SearchWordsByPrefix(id, prefix);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<MatchedWordsResponse>();
        }

        [Theory]
        [InlineData("1")]
        public async Task BookController_SearchWordsByPrefix_OnBadRequest_ThrowsAppException(string id)
        {
            //Arrange
            var mockBookService = new Mock<IBookService>();
            BooksController booksController = new BooksController(mockBookService.Object);

            //Act & Assert
            await Assert.ThrowsAsync<AppException>(async () => await booksController.SearchWordsByPrefix(id, null));
        }

        
    }
}





