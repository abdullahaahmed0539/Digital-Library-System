using DigitalLibrary.Domain;

namespace DigitalLibrary.WebApi.DTOs.Responses
{
    public record GetAllBooksResponse
    {
        public List<bookDetail>? books { get; set; } 
    }

    public record bookDetail
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

    }
}