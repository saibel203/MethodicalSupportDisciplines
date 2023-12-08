using System.Linq.Expressions;

namespace MethodicalSupportDisciplines.Data.Interfaces;

public interface IRepositoryBase
{
    Task<T?> Get<T>(int id)
        where T : class;

    Task<T?> GetAsNoTracking<T>(int id)
        where T : class;

    Task<T?> Get<T>(Expression<Func<T, bool>> predicate)
        where T : class;

    Task<T?> GetAsNoTracking<T>(Expression<Func<T, bool>> predicate)
        where T : class;
    
    Task<IEnumerable<T>> GetAll<T>()
        where T : class;
    
    Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> predicate)
        where T : class;
    
    Task<IEnumerable<T>> GetAllAsTracking<T>()
        where T : class;
    
    Task<IEnumerable<T>> GetAllAsTracking<T>(Expression<Func<T, bool>> predicate)
        where T : class;
}