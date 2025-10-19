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

        public async Task<ClientWithOrderCountDto?> GetClientWithMostOrdersAsync()
        {
            // 1. Obtenemos la ENTIDAD desde el repositorio
            var client = await _unitOfWork.Clients.GetClientWithMostOrdersAsync();
            if (client == null)
            {
                return null;
            }

            // 2. Mapeamos la ENTIDAD al DTO aquí, en la capa de servicio
            return new ClientWithOrderCountDto
            {
                ClientName = client.Name,
                OrderCount = client.Orders.Count()
            };
        }
    }
}