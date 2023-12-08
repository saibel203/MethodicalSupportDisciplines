using System.Linq.Expressions;
using MethodicalSupportDisciplines.Data.Interfaces;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace MethodicalSupportDisciplines.Data.Repositories;

public class RepositoryBase : IRepositoryBase
{
    protected DataDbContext Context { get; }

    protected RepositoryBase(DataDbContext context)
    {
        Context = context;
    }

    public async Task<T?> Get<T>(int id)
        where T : class
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetAsNoTracking<T>(int id) where T : class
    {
        var entity = await Context.Set<T>().FindAsync(id);

        if (entity != null)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<T?> Get<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        return await Context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T?> GetAsNoTracking<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        return await Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> GetAll<T>() where T : class
    {
        return await Context.Set<T>().AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        return await Context.Set<T>().AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsTracking<T>() where T : class
    {
        return await Context.Set<T>()
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsTracking<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        return await Context.Set<T>()
            .Where(predicate)
            .ToListAsync();
    }
}