using RUM.Models;

public class Message
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public User? User { get; set; }
}
