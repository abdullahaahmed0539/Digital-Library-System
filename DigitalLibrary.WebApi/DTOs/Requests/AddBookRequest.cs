
using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.WebApi.DTOs.Requests
{
    public record AddBookRequest
    {
        [Required]
        public string bookName { get; set; } = string.Empty;

        [Required]
        public IFormFile? bookFile { get; set; } = null;
    }
}