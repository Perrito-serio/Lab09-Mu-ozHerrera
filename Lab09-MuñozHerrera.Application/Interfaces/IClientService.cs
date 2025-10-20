using Lab09_MuñozHerrera.Application.DTOs;

namespace Lab09_MuñozHerrera.Application.Interfaces
{
    public interface IClientService
    {
        Task<ClientWithOrderCountDto?> GetClientWithMostOrdersAsync();
        
        Task<IEnumerable<ClientOrderDto>> GetClientsWithOrdersDtoAsync();
        
        Task<IEnumerable<ClientTotalProductsDto>> GetClientsWithTotalProductsDtoAsync();
    }
}