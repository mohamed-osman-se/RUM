using System.ComponentModel.DataAnnotations;

public class AnonymousMessageDTO
{
    public int ReceiverId { get; set; }
    [Required]
    [StringLength(500)] 
    public string? Content { get; set; }
}