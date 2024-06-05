using Microsoft.AspNetCore.Mvc;
using E_Commerce.Interface;
using E_Commerce.DTO;



namespace E_Commerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ProductDTO> AddProduct(ProductDTO productDTO)
        {
            var response = await _productService.AddProduct(productDTO);
            return response;

        }
        [HttpPost]
        public async Task<ProductDTO> GetProductByUId(string UId)
        {
            var response = await _productService.GetProductByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task <List<ProductDTO>> GetProductByCategory(string Category)
        {
            var response = await _productService.GetProductByCategory(Category);
            return response;
        }

        [HttpPost]
        public async Task<List<ProductDTO>> GetAllProduct()
        {
            var response = await _productService.GetAllProduct();
            return response;
        }

        [HttpPost]
        public async Task<ProductDTO> UpdateProduct (ProductDTO productDTO)
        {
            var response = await _productService.UpdateProduct(productDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteProduct (string UId)
        {
            var response = await _productService.DeleteProduct(UId);
            return response;
        }

       
    }
}
