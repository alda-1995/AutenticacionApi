using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProduct
    {
        Task<List<ProductViewModel>> GetProductsAll(int limit);
        Task<List<ProductViewModel>> GetProductsByUserId(string userId);
        Task<ResponseViewModel> CreateProductAsync(CreateProductViewModel productViewModel);
    }
}
