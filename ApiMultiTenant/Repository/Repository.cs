using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApiMultiTenant.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiMultiTenant.Repository
{
     public class Repository<T> where T : class
    {
        private Context _context = null;

        DbSet<T> _DbSet;

        public Repository(Context context)
        {
            _context = context;
            _DbSet = _context.Set<T>();
        }
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", bool readyOnly = false, int skip = 0, int take = 0)
        {
            IQueryable<T> query = _DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (readyOnly)
            {
                query = query.AsNoTracking();
            }

            if (orderBy != null)
            {
                query = orderBy(query);

            }

            if (skip != 0)
            {
                query = query.Skip(skip);
            }

            if (take != 0)
            {
                query = query.Take(take);
            }

            return query.ToList();
        }

      
        public virtual List<TResult> GetFields<TResult>(
            Expression<Func<T, TResult>> fields,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", bool readyOnly = false, int skip = 0, int take = 0) where TResult : class
        {
            IQueryable<T> query = _DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (readyOnly)
            {
                query = query.AsNoTracking();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != 0)
            {
                query = query.Skip(skip);
            }

            if (take != 0)
            {
                query = query.Take(take);
            }

            return query.Select(fields).ToList();
        }


        public virtual T GetByID(object id)
        {
            return _DbSet.Find(id);
        }


        public virtual void Add(T entity)
        {
            _DbSet.Add(entity);
        }

        public virtual void Delete(T entityToDelete)
        {
            _DbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(List<T> entityToDelete)
        {
            _DbSet.RemoveRange(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            _DbSet.Update(entityToUpdate);
        }


        public virtual int Count()
        {
            return _DbSet.Count();
        }
    }
}