using Lab09_MuñozHerrera.Core.Entities;
using Lab09_MuñozHerrera.Application.DTOs;

namespace Lab09_MuñozHerrera.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderProductDetailDto>> GetDetailsByOrderIdAsync(int orderId);
        Task<int> GetTotalQuantityByOrderIdAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date);
        Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync();
    }
}