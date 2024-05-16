namespace ProjectOgnenBozhinov5179.Models.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<string> CategoryNames { get; set; } = new List<string>();

        public decimal Price { get; set; } = decimal.Zero;

        public int Quantity { get; set; } = int.MaxValue;
    }
}
