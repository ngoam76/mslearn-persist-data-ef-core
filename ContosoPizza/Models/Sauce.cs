using System.ComponentModel.DataAnnotations; // allow using mapping attributes

namespace ContosoPizza.Models;

public class Sauce
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    public bool IsVegan { get; set; } 
}