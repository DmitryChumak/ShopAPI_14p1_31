using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services;
using Shop.API.Domain.Repositories;
using Shop.API.Domain.Services.Communication;
using Shop.API.Persistence.Helpers;

namespace Shop.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<PagedList<Product>> ListAsync(ProductParams productParams)
        {
            return await productRepository.ListAsync(productParams);
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            var existingProduct = await productRepository.FindByIdAsync(id);
            if (existingProduct == null)
                return new ProductResponse("Product not found");

            try
            {
                productRepository.Remove(existingProduct);
                await unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Error when deleting product: {ex.Message}");
            }

        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try 
            {
                await productRepository.AddAsync(product);
                await unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Error occured when saving product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            var existingProduct  = await productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse("Product not found");

            existingProduct.Name = product.Name;

            try
            {
                productRepository.Update(existingProduct);
                await unitOfWork.CompleteAsync();
                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Product update error: {ex.Message}");
            }
        }
    }
}