using Lab09_MuñozHerrera.Core.Entities;

namespace Lab09_MuñozHerrera.Core.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<IEnumerable<Client>> FindClientsByNameAsync(string name);
        Task<Client?> GetClientWithMostOrdersAsync();
        Task<IEnumerable<Client>> GetClientsByProductAsync(int productId);
        
        Task<IEnumerable<Client>> GetClientsWithOrdersAsNoTrackingAsync();
        
        Task<IEnumerable<(string ClientName, int TotalProducts)>> GetClientsWithTotalProductsAsync();
        
        Task<IEnumerable<(string ClientName, decimal TotalSales)>> GetTotalSalesByClientAsync();
    }
}