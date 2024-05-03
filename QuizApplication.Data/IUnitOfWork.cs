using Microsoft.EntityFrameworkCore.Storage;

namespace QuizApplication.Data;

public interface IUnitOfWork
{
    Task CommitAsync(IDbContextTransaction transaction);
    Task<IDbContextTransaction> CreateTransactionAsync();
    Task SaveChangesAsync();
}