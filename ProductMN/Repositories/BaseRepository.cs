using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductMN.Repositories
{
    public interface IBaseRepository<E,K> where E : class
    {
        E Create(E e);
        E Update(K k,E e);
        E FindById(K id);
        E Delete(E e);
        IQueryable<E> GetAll();
        IQueryable<E> FindByName(string name);
    }
    public abstract class BaseRepository<E, K> : IBaseRepository<E, K> where E : class
    {
        protected DbContext _context;
        protected DbSet<E> _set;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _set = _context.Set<E>();
        }

        public E Create(E e)
        {
            return _set.Add(e);
        }

        public E Delete(E e)
        {
            return _set.Remove(e);
        }

        public abstract E FindById(K id);

        public abstract IQueryable<E> FindByName(string name);

        public IQueryable<E> GetAll()
        {
            return _set;
        }

        public abstract E Update(K k,E e);
    }
}