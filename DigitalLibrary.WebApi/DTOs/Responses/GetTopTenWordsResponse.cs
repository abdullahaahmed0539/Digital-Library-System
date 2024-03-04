
namespace DigitalLibrary.WebApi.DTOs.Responses
{
    public record GetTopTenWordsResponse
    {
        public string BookName { get; set; } = string.Empty;
        public List<KeyValuePair<string, int>> TopTenWords { get; set; } = null;
    }
}