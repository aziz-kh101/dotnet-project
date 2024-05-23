using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyApp.Namespace
{

    public class CarEditModel
    {
        public string ImageUrl { get; set; } = "";
        public SelectList? Categories { get; set; } = null;
        public int Id { get; set; }
        public CarEditModule Car { get; set; } = new();
        [Required]
        public int SelectedCategoryId { get; set; }
    }

    public class CarEditModule
    {
        [Required]
        public string Model { get; set; } = "";

        [Required]
        public string Brand { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";

        public IFormFile? Image { get; set; } = null;
        [Required]
        [Range(10, double.MaxValue)]
        public double Price { get; set; }
        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }
    }
}
