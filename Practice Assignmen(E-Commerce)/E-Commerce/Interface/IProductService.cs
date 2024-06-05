using E_Commerce.DTO;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Interface
{
    public interface IProductService
    {
        Task<ProductDTO> AddProduct(ProductDTO productDTO);

        Task<ProductDTO> GetProductByUId(string UId);

        Task<List<ProductDTO>> GetProductByCategory(string Category);

        Task<List<ProductDTO>> GetAllProduct();

        Task<ProductDTO> UpdateProduct(ProductDTO productDTO);

        Task<string> DeleteProduct(string uId);

       


      
    }


}
