using Lab09_MuñozHerrera.Core.Entities;
using Lab09_MuñozHerrera.Core.Interfaces;
using Lab09_MuñozHerrera.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab09_MuñozHerrera.Infrastructure.Persistence.Repositories
{
    // Lógica basada en
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetProductsByPriceGreaterThanAsync(decimal price)
        {
            return await _context.Products
                .Where(p => p.Price > price)
                .ToListAsync();
        }

        public async Task<Product?> GetMostExpensiveProductAsync()
        {
            return await _context.Products
                .OrderByDescending(p => p.Price)
                .FirstOrDefaultAsync();
        }

        public async Task<decimal> GetAverageProductPriceAsync()
        {
            return await _context.Products.AverageAsync(p => p.Price);
        }

        public async Task<IEnumerable<Product>> GetProductsWithoutDescriptionAsync()
        {
            return await _context.Products
                .Where(p => string.IsNullOrEmpty(p.Description))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsSoldToClientAsync(int clientId)
        {
            return await _context.Products
                .Where(p => p.Orderdetails.Any(od => od.Order.Clientid == clientId))
                .Distinct()
                .ToListAsync();
        }
    }
}