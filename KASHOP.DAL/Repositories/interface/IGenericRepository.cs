using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.@interface
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        int save(T entity);
        IEnumerable<T> findAll(bool withTracking = false);
        T findById(int id);
        int remove(T entity);
        int update(T entity);
    }
}
