namespace Lab09_MuñozHerrera.Core.Interfaces
{
public interface IUnitOfWork : IDisposable
{

    Task<int> SaveAsync();
}
}