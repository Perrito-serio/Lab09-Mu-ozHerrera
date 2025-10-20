using Lab09_MuñozHerrera.Application.Interfaces; 
using Microsoft.AspNetCore.Mvc;

namespace Lab09_MuñozHerrera.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService; // <-- Pide el SERVICIO

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/clients/most-orders
        [HttpGet("most-orders")]
        public async Task<IActionResult> GetClientWithMostOrders()
        {
            var clientDto = await _clientService.GetClientWithMostOrdersAsync();
            if (clientDto == null)
            {
                return NotFound("No se encontraron clientes con órdenes.");
            }
            return Ok(clientDto);
        }
        
        // GET: api/clients/with-orders-asnotracking
        [HttpGet("with-orders-asnotracking")]
        public async Task<IActionResult> GetClientsWithOrdersAsNoTracking()
        {
            var clientsDto = await _clientService.GetClientsWithOrdersDtoAsync();
            return Ok(clientsDto);
        }
        
        // GET: api/clients/with-total-products
        [HttpGet("with-total-products")]
        public async Task<IActionResult> GetClientsWithTotalProducts()
        {
            var clientsDto = await _clientService.GetClientsWithTotalProductsDtoAsync();
            return Ok(clientsDto);
        }
        
        // GET: api/clients/total-sales
        [HttpGet("total-sales")]
        public async Task<IActionResult> GetTotalSalesByClient()
        {
            var salesDto = await _clientService.GetTotalSalesByClientDtoAsync();
            return Ok(salesDto);
        }
    }
}