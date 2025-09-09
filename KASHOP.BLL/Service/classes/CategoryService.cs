using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
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
    public class CategoryService : GenericService<CategoryRequest,CategoryResponse,Category>
        , ICategoryService
    {
        public CategoryService(ICategoryRepository repository):base(repository)
        {
            
        }
    }
}
