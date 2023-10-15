using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Interfaces.Repository;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        Context _context;
        public Repository(Context context)
        {
            _context = context;
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return (T)_context.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);  
           return entity;
        }

        public  void Delete(int id)
        {
           var entity =  _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
        }
    }
}
