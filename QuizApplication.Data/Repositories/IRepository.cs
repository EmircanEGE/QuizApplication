using System.Linq.Expressions;

namespace QuizApplication.Data.Repositories;

public interface IRepository<T>
{
    Task InsertAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> GetAsync(Expression<Func<T, bool>> expression);
}