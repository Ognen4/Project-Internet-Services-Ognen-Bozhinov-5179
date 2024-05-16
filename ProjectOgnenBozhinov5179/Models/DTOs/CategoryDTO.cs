using System.ComponentModel.DataAnnotations;

namespace ProjectOgnenBozhinov5179.Models.DTOs
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
