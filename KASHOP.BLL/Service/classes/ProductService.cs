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
using Microsoft.AspNetCore.Http;

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

            if (productRequest.subImages != null)
            {
                var subImagesPaths = await _fileService.UploadManyAsync(productRequest.subImages);
                entity.subImages = subImagesPaths.Select(img => new ProductImage { ImageName = img }).ToList();

            }

            return _productRepository.save(entity);
        }

        public async Task<List<ProductResponse>> GetAllProduct(HttpRequest httpRequest,bool onlyActive = false)
        {
            var products = await _productRepository.GetAllProductsWithImages();
            if (onlyActive)
            {
                products = products.Where(p => p.Status == Status.Active).ToList();
            }
            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Quantity = p.Quantity,
                MainImage = $"{httpRequest.Scheme}://{httpRequest.Host}/images/{ p.MainImage}",
                SubImagesUrl = p.subImages.Select(img => $"{httpRequest.Scheme}://{httpRequest.Host}/images/{img.ImageName}").ToList()
            }).ToList();
        }
    }
}
