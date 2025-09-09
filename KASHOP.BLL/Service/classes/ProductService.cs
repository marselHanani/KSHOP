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
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository, IFileService fileService) : base(productRepository)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateWithFile(ProductRequest productRequest)
        {
            var entity = productRequest.Adapt<Product>();
            entity.CreatedAt = DateTime.UtcNow;
            if(productRequest.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(productRequest.MainImage);
                entity.MainImage = imagePath;
            }

            return _productRepository.save(entity);
        }
    }
}
