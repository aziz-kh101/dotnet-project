using System.ComponentModel.DataAnnotations;

namespace MyApp.Namespace
{
    public class CategoryEditModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
    }
}
