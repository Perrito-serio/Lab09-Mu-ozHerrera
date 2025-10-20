using Lab09_MuñozHerrera.Application.DTOs;
using Lab09_MuñozHerrera.Application.Interfaces;
using Lab09_MuñozHerrera.Core.Entities;
using Lab09_MuñozHerrera.Core.Interfaces;

namespace Lab09_MuñozHerrera.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderProductDetailDto>> GetDetailsByOrderIdAsync(int orderId)
        {
            var details = await _unitOfWork.OrderDetails.GetDetailsByOrderIdAsync(orderId);

            return details.Select(od => new OrderProductDetailDto
            {
                ProductName = od.Product.Name,
                Quantity = od.Quantity
            }).ToList();
        }

        public Task<int> GetTotalQuantityByOrderIdAsync(int orderId)
        {
            return _unitOfWork.OrderDetails.GetTotalQuantityByOrderIdAsync(orderId);
        }

        public Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date)
        {
            return _unitOfWork.Orders.GetOrdersAfterDateAsync(date);
        }

        public async Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllOrdersWithDetailsAsync();

            return orders.Select(o => new OrderWithDetailsDto
            {
                OrderId = o.Orderid,
                OrderDate = o.Orderdate,
                ClientName = o.Client.Name,
                Details = o.Orderdetails.Select(od => new OrderProductDetailDto
                {
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity
                }).ToList()
            }).ToList();
        }
        
        public async Task<IEnumerable<OrderDetailsDto>> GetOrdersWithDetailsAndProductsDtoAsync()
        {
            var orders = await _unitOfWork.Orders.GetOrdersWithDetailsAndProductsAsync();

            return orders.Select(order => new OrderDetailsDto
            {
                OrderId = order.Orderid,
                OrderDate = order.Orderdate,
                Products = order.Orderdetails.Select(od => new ProductDto
                {
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity,
                    Price = od.Product.Price
                }).ToList()
            });
        }
    }
}