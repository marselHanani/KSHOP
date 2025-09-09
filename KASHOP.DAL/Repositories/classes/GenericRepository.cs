using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.@interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.classes
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<T> findAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _context.Set<T>().ToList();
            }
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T findById(int id)
        {
            return _context.Set<T>().Find(id) ?? throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }

        public int remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        public int save(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public int update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
    }
}
