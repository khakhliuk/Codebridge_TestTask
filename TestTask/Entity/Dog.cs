namespace Codebridge_TestTask.Entity;

public record Dog
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
    public int TailLenght { get; set; }
    public int Weight { get; set; }
}