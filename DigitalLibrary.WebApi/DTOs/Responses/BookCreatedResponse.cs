using DigitalLibrary.Domain;

namespace DigitalLibrary.WebApi.DTOs.Responses
{
    public record CreateBookResponse
    {
        public bookDetail? BookCreated { get; set; } = null;
    }
}