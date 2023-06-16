namespace Codebridge_TestTask.Models;

public record CreateDogRequest
{
    public string? Name { get; set; }
    public string? Color { get; set; }
    public int TailLenght { get; set; }
    public int Weight { get; set; }
}