using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CSM1.Core.Entities.Common;

namespace CSM1.Business.Repositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; }
    IQueryable<T> GetAll(bool noTracking = true, params string[] includes);
    Task<T> GetByIdAsync(int id, bool noTracking = true, params string[] includes);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task CreateAsync(T data);
    void Remove(T data, bool soft = true);
    Task SaveAsync();
}
