using CityForum.Entities.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CityForum.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private DbContext _context;
    private ILogger<Repository<T>> _logger;

    public Repository(DbContext context, ILogger<Repository<T>> logger)
    {
        _context = context;
        _logger = logger;
    }
    public void Delete(T obj)
    {
        _context.Set<T>().Attach(obj);
        _context.Entry(obj).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate);
    }

    public T? GetById(Guid id)
    {
        return _context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    private T Insert(T obj)
    {
        obj.Init();
        var result = _context.Set<T>().Add(obj);
        _context.SaveChanges();
        return result.Entity;
    }

    private T Update(T obj)
    {
        obj.ModificationTime = DateTime.UtcNow;
        var result = _context.Set<T>().Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
        _context.SaveChanges();
        return result.Entity;
    }

    public T Save(T obj)
    {
        try
        {
            if (obj.IsNew())
            {
                return Insert(obj);
            }
            else
            {
                return Update(obj);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw e;
        }
    }
}
