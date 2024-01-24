using System.ComponentModel.DataAnnotations;
namespace ContosoPizza.Models;

// create entity classes

public class Pizza
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    public Sauce? Sauce { get; set; }
    
    public ICollection<Topping>? Toppings { get; set; } // many-to-many relationship with pizza
}