namespace ManoelStore.Models
{
    public class BoxRequest
    {
        public string TypeBox { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new();
    }


    public class RequestResponse
    {
        public int OrderId { get; set; }
        public List<BoxRequest> BoxesUsed { get; set; } = new();
    }


}