namespace DigitalLibrary.Domain;
public class Book
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Content{ get; set; } = string.Empty;
    public Dictionary<string, int>? WordsCount { get; set; } = null;
}
