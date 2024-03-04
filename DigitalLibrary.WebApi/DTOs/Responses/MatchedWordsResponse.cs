
namespace DigitalLibrary.WebApi.DTOs.Responses
{
    public record MatchedWordsResponse
    {
        public List<KeyValuePair<string, int>>? MatchedWords { get; set; } = null;
    }
}