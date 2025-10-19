using Lab09_MuñozHerrera.Application.Interfaces;
using Lab09_MuñozHerrera.Core.Entities;
using Lab09_MuñozHerrera.Core.Interfaces;

namespace Lab09_MuñozHerrera.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
        public Task<IEnumerable<Product>> GetProductsByPriceGreaterThanAsync(decimal price)
        {
            return _unitOfWork.Products.GetProductsByPriceGreaterThanAsync(price);
        }

        public Task<Product?> GetMostExpensiveProductAsync()
        {
            return _unitOfWork.Products.GetMostExpensiveProductAsync();
        }

        public Task<decimal> GetAverageProductPriceAsync()
        {
            return _unitOfWork.Products.GetAverageProductPriceAsync();
        }

        public Task<IEnumerable<Product>> GetProductsWithoutDescriptionAsync()
        {
            return _unitOfWork.Products.GetProductsWithoutDescriptionAsync();
        }

        public Task<IEnumerable<Product>> GetProductsSoldToClientAsync(int clientId)
        {
            return _unitOfWork.Products.GetProductsSoldToClientAsync(clientId);
        }
    }
}