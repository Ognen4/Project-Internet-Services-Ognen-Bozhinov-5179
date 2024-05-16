using System.ComponentModel.DataAnnotations;

namespace ProjectOgnenBozhinov5179.Models
{
    public class Category
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
