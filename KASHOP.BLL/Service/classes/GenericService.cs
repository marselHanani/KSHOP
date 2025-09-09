using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.@interface;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> _genericRepo;

        public GenericService(IGenericRepository<TEntity> genericRepo)
        {
            _genericRepo = genericRepo;
        }
        public int Add(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return _genericRepo.save(entity);
        }

        public int Delete(int id)
        {
            var entity = _genericRepo.findById(id);
            if(entity == null)
            {
                throw new KeyNotFoundException($"{nameof(entity)} with ID {id} not found.");
            }
            return _genericRepo.remove(entity);
        }

        public IEnumerable<TResponse> GetAll(bool isActive = false)
        {
           var entities = _genericRepo.findAll();
            if (isActive)
            {
               entities = entities.Where(e => e.Status == Status.Active);
            }
           return entities.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(int id)
        {
            var entity = _genericRepo.findById(id);
            return entity.Adapt<TResponse>();
        }

        public bool ToggleStatus(int id)
        {
            var entity = _genericRepo.findById(id);
            if (entity == null) return false;
            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active;
            _genericRepo.update(entity);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var entity = _genericRepo.findById(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"{nameof(TEntity)} with ID {id} not found.");
            }
            var updatedEntity = request.Adapt(entity);
            return _genericRepo.update(updatedEntity);
        }
    }
}
