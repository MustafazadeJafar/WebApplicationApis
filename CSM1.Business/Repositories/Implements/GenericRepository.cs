using CSM1.Business.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CSM1.Core.Entities.Common;
using CSM1.DAL.Contexts;

namespace CSM1.Business.Repositories.Implements;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    CSM1DbContext _context { get; }

    public GenericRepository(CSM1DbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool noTracking = true, params string[] includes)
        => noTracking ? Include(Table.AsNoTracking(), includes) : Include(Table, includes);

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
    {
        return await Table.AnyAsync(expression);
    }

    public async Task CreateAsync(T data)
    {
        await Table.AddAsync(data);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<T> GetByIdAsync(int id, bool noTracking = true, params string[] includes)
    {
        if (noTracking || includes != null) 
        {
            IQueryable<T> query = noTracking ? Table.AsNoTracking() : Table;
            return await Include(query, includes).SingleOrDefaultAsync(t => t.Id == id);
        }
        return await Table.FindAsync(id);
    }
    public IQueryable<T> Include(IQueryable<T> query, params string[] includes)
    {
        if (includes?.Length > 0)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }
        return query;
    }


    public void Remove(T data, bool soft = true)
    {
        if (!soft) Table.Remove(data);
        else if (data.IsActive)
        {
            data.IsActive = false;
            Table.Update(data);
        }
    }
}
