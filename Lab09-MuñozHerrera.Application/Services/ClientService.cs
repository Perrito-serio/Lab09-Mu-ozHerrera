using Lab09_MuñozHerrera.Application.DTOs;
using Lab09_MuñozHerrera.Application.Interfaces;
using Lab09_MuñozHerrera.Core.Interfaces; 

namespace Lab09_MuñozHerrera.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        // Implementación del método GetClientWithMostOrdersAsync
        public async Task<ClientWithOrderCountDto?> GetClientWithMostOrdersAsync()
        {
            var client = await _unitOfWork.Clients.GetClientWithMostOrdersAsync();
            if (client == null)
            {
                return null;
            }

            return new ClientWithOrderCountDto
            {
                ClientName = client.Name,
                OrderCount = client.Orders.Count()
            };
        }
        
        // Implementación del método GetClientsWithOrdersDtoAsync
        public async Task<IEnumerable<ClientOrderDto>> GetClientsWithOrdersDtoAsync()
        {
            var clients = await _unitOfWork.Clients.GetClientsWithOrdersAsNoTrackingAsync();

            return clients.Select(client => new ClientOrderDto
            {
                ClientName = client.Name,
                Orders = client.Orders.Select(order => new OrderDto
                {
                    OrderId = order.Orderid,
                    OrderDate = order.Orderdate
                }).ToList()
            });
        }
    }
}