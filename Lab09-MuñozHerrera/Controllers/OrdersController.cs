using Lab09_MuñozHerrera.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab09_MuñozHerrera.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders/1/details
        [HttpGet("{orderId}/details")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var details = await _orderService.GetDetailsByOrderIdAsync(orderId);
            return Ok(details);
        }

        // GET: api/orders/1/total-quantity
        [HttpGet("{orderId}/total-quantity")]
        public async Task<IActionResult> GetTotalQuantity(int orderId)
        {
            var total = await _orderService.GetTotalQuantityByOrderIdAsync(orderId);
            return Ok(new { orderId = orderId, totalQuantity = total });
        }

        // GET: api/orders/after-date?date=2025-05-03
        [HttpGet("after-date")]
        public async Task<IActionResult> GetOrdersAfterDate([FromQuery] DateTime date)
        {
            var orders = await _orderService.GetOrdersAfterDateAsync(date);
            return Ok(orders);
        }

        // GET: api/orders/with-details
        [HttpGet("with-details")]
        public async Task<IActionResult> GetAllOrdersWithDetails()
        {
            var orders = await _orderService.GetAllOrdersWithDetailsAsync();
            return Ok(orders);
        }
    }
}