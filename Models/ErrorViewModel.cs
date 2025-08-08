using System.Collections.Generic;

public class ErrorViewModel
{
    public int StatusCode { get; set; }
    public string? Title { get; set; }  // Optional: Short summary
    public List<string> Messages { get; set; } = new(); // Detailed errors
}
