using Lab09_MuñozHerrera.Core.Entities;

namespace Lab09_MuñozHerrera.Core.Interfaces
{
    public interface IOrderDetailRepository : IGenericRepository<Orderdetail>
    {
        Task<IEnumerable<Orderdetail>> GetDetailsByOrderIdAsync(int orderId);
        Task<int> GetTotalQuantityByOrderIdAsync(int orderId);
    }
}