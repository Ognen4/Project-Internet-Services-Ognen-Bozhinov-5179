using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectOgnenBozhinov5179.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
