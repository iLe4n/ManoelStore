
using Microsoft.AspNetCore.Mvc;
using ManoelStore.Models;
using ManoelStore.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManoelStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly StoreDbContext _context; //Inject the DbContext

        public OrdersController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrders([FromBody] List<Order> requestedOrders)
        {
            if (requestedOrders == null || !requestedOrders.Any())
            {
                return BadRequest("A lista de pedidos não pode ser vazia.");
            }

            var availableBoxes = new List<(string Name, int H, int W, int L)>
            {
                ("Box I", 30, 40, 80),
                ("Box II", 80, 50, 40),
                ("Box III", 50, 80, 60)
            };

            var responseList = new List<RequestResponse>();

            foreach (var requestedOrderData in requestedOrders)
            {

                var dbOrder = new Order
                {
                    OrderId = requestedOrderData.OrderId, //OrderId
                    Products = new List<Product>()
                };

                //Map request products to database Product entities
                foreach (var reqProduct in requestedOrderData.Products)
                {
                    dbOrder.Products.Add(new Product
                    {

                        Height = reqProduct.Height,
                        Width = reqProduct.Width,
                        Length = reqProduct.Length

                    });
                }

                var result = new RequestResponse
                {
                    OrderId = dbOrder.OrderId
                };

                var usedBoxes = new Dictionary<string, List<Product>>
                {
                    { "Box I", new List<Product>() },
                    { "Box II", new List<Product>() },
                    { "Box III", new List<Product>() }
                };


                foreach (var product in dbOrder.Products)
                {
                    bool packed = false;
                    foreach (var box in availableBoxes)
                    {
                        if (product.Height <= box.H && product.Width <= box.W && product.Length <= box.L)
                        {
                            usedBoxes[box.Name].Add(product);
                            packed = true;
                            break;
                        }
                    }

                    if (!packed)
                    {
                        result.BoxesUsed.Add(new BoxRequest
                        {
                            TypeBox = "Sorry, not fit in any box :( )",
                            Products = new List<Product> { product }
                        });
                    }
                }

                foreach (var boxEntry in usedBoxes)
                {
                    if (boxEntry.Value.Any())
                    {
                        result.BoxesUsed.Add(new BoxRequest
                        {
                            TypeBox = boxEntry.Key,
                            Products = boxEntry.Value
                        });
                    }
                }
                responseList.Add(result);
            }


            await _context.SaveChangesAsync();

            return Ok(responseList);
        }
    }
}