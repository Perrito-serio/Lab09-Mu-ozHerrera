using Lab09_MuñozHerrera.Core.Entities; 

namespace Lab09_MuñozHerrera.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsByPriceGreaterThanAsync(decimal price);
        Task<Product?> GetMostExpensiveProductAsync();
        Task<decimal> GetAverageProductPriceAsync();
        Task<IEnumerable<Product>> GetProductsWithoutDescriptionAsync();
        Task<IEnumerable<Product>> GetProductsSoldToClientAsync(int clientId);
    }
}