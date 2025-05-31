
using System.Collections.Generic;

namespace ManoelStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}