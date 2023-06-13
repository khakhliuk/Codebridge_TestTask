namespace Codebridge_TestTask.Models;

public class UpdateDogRequest
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
    public int TailLenght { get; set; }
    public int Weight { get; set; }
}