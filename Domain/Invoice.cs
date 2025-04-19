
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Invoice
{
    [Key]
    public int Id { get; set; } // Clave primaria

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

