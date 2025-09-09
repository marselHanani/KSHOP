using KASHOP.BLL.Service.interfaces;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.classes;
using KASHOP.DAL.Repositories.@interface;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.classes
{
    public class BrandService : GenericService<BrandRequest,BrandResponse,Brand> , IBrandService
    {
        
        private readonly IBrandRepository _repository;
        private readonly IFileService _fileService;

        public BrandService(IBrandRepository repository, IFileService fileService) : base(repository)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<int> CreateWithFile(BrandRequest request)
        {
            var entity = request.Adapt<Brand>();
            entity.CreatedAt = DateTime.UtcNow;
            if (request.ImageUrl != null)
            {
                var imagePath = await _fileService.UploadAsync(request.ImageUrl);
                entity.ImageUrl = imagePath;
            }

            return _repository.save(entity);
        }
    }
}
