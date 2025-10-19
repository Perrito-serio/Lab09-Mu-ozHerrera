using Lab09_MuñozHerrera.Core.Interfaces;
using Lab09_MuñozHerrera.Infrastructure.Persistence.Data;


namespace Lab09_MuñozHerrera.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IClientRepository _clients;
        private IProductRepository _products;
        private IOrderRepository _orders;
        private IOrderDetailRepository _orderDetails;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IClientRepository Clients => _clients ??= new ClientRepository(_context);
        public IProductRepository Products => _products ??= new ProductRepository(_context);
        public IOrderRepository Orders => _orders ??= new OrderRepository(_context);
        public IOrderDetailRepository OrderDetails => _orderDetails ??= new OrderDetailRepository(_context);

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}