using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyApp.Namespace
{
    public class CarCreateModel
    {
        public CarModule Car { get; set; } = new();
        public SelectList? Categories { get; set; } = null;
        [Required]
        public int SelectedCategoryId { get; set; }
    }

    public class CarModule
    {
        [Required]
        public string Model { get; set; } = "";

        [Required]
        public string Brand { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        [Required]
        public IFormFile? Image { get; set; } = null;
        [Required]
        [Range(10, double.MaxValue)]
        public double Price { get; set; }
        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }
    }
}
