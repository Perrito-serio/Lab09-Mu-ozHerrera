using Lab09_MuñozHerrera.Core.Entities;
using Lab09_MuñozHerrera.Core.Interfaces;
using Lab09_MuñozHerrera.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Lab09_MuñozHerrera.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Client>> FindClientsByNameAsync(string name)
        {
            return await _context.Clients
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<Client?> GetClientWithMostOrdersAsync()
        {
            return await _context.Clients
                .Include(c => c.Orders)
                .OrderByDescending(c => c.Orders.Count())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsByProductAsync(int productId)
        {
            return await _context.Clients
                .Where(c => c.Orders.Any(o => o.Orderdetails.Any(od => od.Productid == productId)))
                .Distinct()
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Client>> GetClientsWithOrdersAsNoTrackingAsync()
        {
            return await _context.Clients
                .AsNoTracking() 
                .Include(c => c.Orders) 
                .ToListAsync();
        }
        
        public async Task<IEnumerable<(string ClientName, int TotalProducts)>> GetClientsWithTotalProductsAsync()
        {
            var results = await _context.Clients
                .AsNoTracking()
                .Include(c => c.Orders)
                .ThenInclude(o => o.Orderdetails)
                .Select(client => new 
                {
                    ClientName = client.Name,
                    TotalProducts = client.Orders
                        .SelectMany(order => order.Orderdetails)
                        .Sum(detail => detail.Quantity)
                })
                .ToListAsync();

            return results.Select(result => (result.ClientName, result.TotalProducts));
        }
        
        public async Task<IEnumerable<(string ClientName, decimal TotalSales)>> GetTotalSalesByClientAsync()
        {
            var salesData = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Orderdetails) 
                .ThenInclude(od => od.Product) 
                .GroupBy(o => o.Clientid) 
                .Select(group => new
                {
                    ClientId = group.Key, 
                    TotalSales = group
                        .SelectMany(order => order.Orderdetails) 
                        .Sum(detail => detail.Quantity * detail.Product.Price) 
                })
                .OrderByDescending(s => s.TotalSales) 
                .ToListAsync(); 

            var clientIds = salesData.Select(s => s.ClientId).ToList();
            var clients = await _context.Clients
                .Where(c => clientIds.Contains(c.Clientid))
                .ToDictionaryAsync(c => c.Clientid, c => c.Name); 

            return salesData.Select(s => (
                ClientName: clients.ContainsKey(s.ClientId) ? clients[s.ClientId] : "Unknown", 
                TotalSales: s.TotalSales
            ));
        }
    }
}