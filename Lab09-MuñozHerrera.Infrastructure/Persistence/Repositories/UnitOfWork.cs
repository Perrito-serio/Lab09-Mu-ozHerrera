using Lab09_MuñozHerrera.Core.Interfaces;
using Lab09_MuñozHerrera.Infrastructure.Persistence.Data;

namespace Lab09_MuñozHerrera.Infrastructure.Persistence.Repositories
{
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    // Implementación de Dispose para liberar el DbContext
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