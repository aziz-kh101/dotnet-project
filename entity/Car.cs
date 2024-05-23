using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project;

public class Car
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Model { get; set; } = "";
    public string Brand { get; set; } = "";
    public string Description { get; set; } = "";
    public string Image { get; set; } = "";
    public double Price { get; set; }
    public int Year { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
