using Lab09_MuñozHerrera.Core.Entities;

namespace Lab09_MuñozHerrera.Core.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date);
        Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync();
    }
}