namespace ProjectOgnenBozhinov5179.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;

        public int Quantity { get; set; } = int.MaxValue;
    }
}
