namespace ManoelStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public int OrderId { get; set; }
    }
}
//PascalCase, standard in C#