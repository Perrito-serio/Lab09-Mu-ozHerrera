using Lab09_MuñozHerrera.Core.Entities;
using Lab09_MuñozHerrera.Core.Interfaces;
using Lab09_MuñozHerrera.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab09_MuñozHerrera.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date)
        {
            return await _context.Orders
                .Where(o => o.Orderdate > date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync()
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Orderdetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Order>> GetOrdersWithDetailsAndProductsAsync()
        {
            return await _context.Orders
                .Include(order => order.Orderdetails)
                .ThenInclude(od => od.Product) 
                .AsNoTracking()
                .ToListAsync();
        }
    }
}