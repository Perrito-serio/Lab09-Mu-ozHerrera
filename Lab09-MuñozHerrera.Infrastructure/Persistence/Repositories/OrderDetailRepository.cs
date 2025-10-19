using Lab09_MuñozHerrera.Core.Entities;
using Lab09_MuñozHerrera.Core.Interfaces;
using Lab09_MuñozHerrera.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab09_MuñozHerrera.Infrastructure.Persistence.Repositories
{
    // Lógica basada en
    public class OrderDetailRepository : GenericRepository<Orderdetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Orderdetail>> GetDetailsByOrderIdAsync(int orderId)
        {
            return await _context.Orderdetails
                .Include(od => od.Product)
                .Where(od => od.Orderid == orderId)
                .ToListAsync();
        }

        public async Task<int> GetTotalQuantityByOrderIdAsync(int orderId)
        {
            return await _context.Orderdetails
                .Where(od => od.Orderid == orderId)
                .SumAsync(od => od.Quantity);
        }
    }
}