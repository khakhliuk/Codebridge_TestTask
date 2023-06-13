namespace Codebridge_TestTask.Models;

public class SearchQuery
{
    public string? Attribute { get; set; }
    public string? Order { get; set; }
    public int PageNumber { get; set; }
    public int Size { get; set; }
}