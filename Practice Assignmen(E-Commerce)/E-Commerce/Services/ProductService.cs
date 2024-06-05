using E_Commerce.Common;
using E_Commerce.CosmosDB;
using E_Commerce.DTO;
using E_Commerce.Common;
using E_Commerce.CosmosDB;
using E_Commerce.Entity;
using E_Commerce.Interface;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly ICosmosDBService _cosmosDBService;

        public ProductService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<ProductDTO> AddProduct(ProductDTO productDTO)
        {
            ProductEntity product = new ProductEntity
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity,
                Category = productDTO.Category
            };

            product.Initalize(true, Credentials.ProductDocumentType, "Shruti", "Shruti");

            var response = await _cosmosDBService.AddProduct(product);

            ProductDTO responseDTO = new ProductDTO
            {
                UId = response.UId,
                Name = response.Name,
                Description = response.Description,
                Price = response.Price,
                Quantity = response.Quantity,
                Category = response.Category
            };

            return responseDTO;


        }

        public async Task<ProductDTO> GetProductByUId(string UId)
        {
            var response = await _cosmosDBService.GetProductByUId(UId);


            ProductDTO productDTO = new ProductDTO
            {
                UId = response.UId,
                Name = response.Name,
                Description = response.Description,
                Price = response.Price,
                Quantity = response.Quantity,
            };
            return productDTO;



        }

        public async Task<List<ProductDTO>> GetProductByCategory(string Category)
        {
            var products = await _cosmosDBService.GetProductByCategory(Category);

            var productList = products.Select(product => new ProductDTO
            {
                UId = product.UId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Category = product.Category
            }).ToList();

            return productList;
        }

        public async Task<List<ProductDTO>> GetAllProduct()
        {
            var products = await _cosmosDBService.GetAllProduct();


            var productList = new List<ProductDTO>();

            foreach (var product in products)
            {
                var productDTO = new ProductDTO();
                productDTO.UId = product.UId;
                productDTO.Name = product.Name;
                productDTO.Description = product.Description;
                productDTO.Price = product.Price;
                productDTO.Quantity = product.Quantity;
                productDTO.Category = product.Category;
                productList.Add(productDTO);
            }
            return productList;
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO productDTO)
        {
            var existingProduct = await _cosmosDBService.GetProductByUId(productDTO.UId);
            existingProduct.Active = true;
            existingProduct.Archived = false;


            await _cosmosDBService.ReplaceAsync(existingProduct);


            existingProduct.Initalize(false, Credentials.ProductDocumentType, "shruti", "shruti");

            existingProduct.Name = productDTO.Name;
            existingProduct.Description = productDTO.Description;
            existingProduct.Price = productDTO.Price;
            existingProduct.Quantity = productDTO.Quantity;
            existingProduct.Category = productDTO.Category;
            var response = await _cosmosDBService.AddProduct(existingProduct);

            var responseDTO = new ProductDTO
            {
                UId = productDTO.UId,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity,
                Category = productDTO.Category,

            };
            return responseDTO;


        }

        public async Task<string> DeleteProduct(string uId)
        {
            var product = await _cosmosDBService.GetProductByUId(uId);
            product.Active = false;
            product.Archived = true;

            await _cosmosDBService.ReplaceAsync(product);

            product.Initalize(false, Credentials.ProductDocumentType, "shruti", "shruti");
            product.Archived = true;

            var response = await _cosmosDBService.AddProduct(product);
            return "Record Deleted";

        }





    }
}

       

        


        
