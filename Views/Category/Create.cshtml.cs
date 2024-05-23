
using System.ComponentModel.DataAnnotations;

namespace MyApp.Namespace
{
    public class CategoryCreateModel
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
    }
}
