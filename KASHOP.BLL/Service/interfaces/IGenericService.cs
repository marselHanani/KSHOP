using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.interfaces
{
    public interface IGenericService<TRequest,TResponse,TEntity> where TEntity: BaseModel
    {
        int Add(TRequest request);
        TResponse? GetById(int id);

        IEnumerable<TResponse> GetAll(bool isActive = false);

        int Update(int id, TRequest request);

        int Delete(int id);
        bool ToggleStatus(int id);
    }
}
