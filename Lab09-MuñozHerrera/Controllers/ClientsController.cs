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
            var clientDto = await _clientService.GetClientWithMostOrdersAsync(); // <-- Llama al SERVICIO
            if (clientDto == null)
            {
                return NotFound("No se encontraron clientes con órdenes.");
            }
            return Ok(clientDto);
        }
    }
}