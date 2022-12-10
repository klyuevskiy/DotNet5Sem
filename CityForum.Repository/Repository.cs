using CityForum.Entities.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CityForum.Entities;
using CityForum.Shared.Exceptions;

namespace CityForum.Repository;

public class Repository<T> : IRepository<T> where T : class, IBaseEntity
{
    private Context context;
    private ILogger<Repository<T>> logger;

    public Repository(Context context, ILogger<Repository<T>> logger)
    {
        this.context = context;
        this.logger = logger;
    }
    public void Delete(T obj)
    {
        context.Set<T>().Attach(obj);
        context.Entry(obj).State = EntityState.Deleted;
        context.SaveChanges();
    }

    public IQueryable<T> GetAll()
    {
        return context.Set<T>();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().Where(predicate);
    }

    public T? GetById(Guid id)
    {
        return context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    private T Insert(T obj)
    {
        obj.Init();
        var result = context.Set<T>().Add(obj);
        context.SaveChanges();
        return result.Entity;
    }

    private T Update(T obj)
    {
        obj.ModificationTime = DateTime.UtcNow;
        var result = context.Set<T>().Attach(obj);
        context.Entry(obj).State = EntityState.Modified;
        context.SaveChanges();
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
            throw new RepositoryException(e.ToString());
        }
    }
}
