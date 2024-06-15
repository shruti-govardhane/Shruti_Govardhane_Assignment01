using Microsoft.AspNetCore.Mvc;
using E_Commerce.Interface;
using E_Commerce.DTO;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;



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

        private string GetStringFormCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue?.ToString()?.Trim();
        }

        [HttpPost]

        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is Empty!!!!");


            var products = new List<ProductDTO>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;


            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;


                    for (int row = 2; row <= rowCount; row++)
                    {
                        var price = Convert.ToInt32(GetStringFormCell(worksheet, row, 4) ?? "0");
                        var quantity = Convert.ToInt32(GetStringFormCell(worksheet, row, 5) ?? "0");
                        var product = new ProductDTO
                        {
                            UId= GetStringFormCell(worksheet, row, 1),
                            Name = GetStringFormCell(worksheet, row, 2),
                            Description= GetStringFormCell(worksheet, row, 3),
                            Category= GetStringFormCell(worksheet, row,6),
                            Price= price,
                            Quantity= quantity,
                        };
                        await AddProduct(product);

                        products.Add(product);
                    }
                }
            }
            return Ok((products));
        }


        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var products = await _productService.GetAllProduct();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("products");

                worksheet.Cells[1, 1].Value = "UId";
                worksheet.Cells[1, 2].Value = "Name"; 
                worksheet.Cells[1, 3].Value = "Description";
                worksheet.Cells[1, 4].Value = "Price";
                worksheet.Cells[1, 5].Value = "Quantity";
                worksheet.Cells[1, 6].Value = "Category"; 
   

                using (var range = worksheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Blue);

                }

                

                for (int i = 0; i < products.Count; i++)
                {
                    var product = products[i];
                    worksheet.Cells[i + 2, 1].Value = product.UId;
                    worksheet.Cells[i + 2, 2].Value = product.Name;
                    worksheet.Cells[i + 2, 3].Value = product.Description;
                    worksheet.Cells[i + 2, 4].Value = product.Price;
                    worksheet.Cells[i + 2, 5].Value = product.Quantity;
                    worksheet.Cells[i + 2, 6].Value = product.Category;


                }

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Product.xlsx";
                return File(stream, "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

            }
        }
    }
}
