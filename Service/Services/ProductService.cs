using BD;
using BD.Models;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using Service.ViewModels;

namespace Service.Services
{
    public class ProductService : IProduct
    {
        private readonly BdContext _bdContext;
        public ProductService(BdContext bdContext)
        {
            _bdContext = bdContext;
        }

        public async Task<ResponseViewModel> CreateProductAsync(CreateProductViewModel productViewModel)
        {
            try
            {
                var newProduct = new Product
                {
                    Name = productViewModel.Name,
                    Price = productViewModel.Price,
                    ApplicationUserId = productViewModel.UserId
                };
                _bdContext.Products.Add(newProduct);
                await _bdContext.SaveChangesAsync();
                return new ResponseViewModel { Message = "Add Product Sucess", Success = true };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel { Message = "Error in create product", Success = false };
            }
        }

        public async Task<List<ProductViewModel>> GetProductsAll(int limit)
        {
            try
            {
                var listProducts = await _bdContext.Products.Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price
                }).Take(limit).ToListAsync();
                return listProducts;
            }
            catch (Exception ex)
            {
                return new List<ProductViewModel>();
            }
        }

        public async Task<List<ProductViewModel>> GetProductsByUserId(string userId)
        {
            try
            {
                var listProducts = await _bdContext.Products.Where(p => p.ApplicationUserId.Equals(userId)).Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price
                }).Take(100).ToListAsync();
                return listProducts;
            }
            catch (Exception ex)
            {
                return new List<ProductViewModel>();
            }
        }
    }
}
