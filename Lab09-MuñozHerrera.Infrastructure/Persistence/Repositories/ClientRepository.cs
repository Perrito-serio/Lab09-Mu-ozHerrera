using Lab09_MuñozHerrera.Core.Entities;
using Lab09_MuñozHerrera.Core.Interfaces;
using Lab09_MuñozHerrera.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab09_MuñozHerrera.Infrastructure.Persistence.Repositories
{
    // Lógica basada en
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
    }
}