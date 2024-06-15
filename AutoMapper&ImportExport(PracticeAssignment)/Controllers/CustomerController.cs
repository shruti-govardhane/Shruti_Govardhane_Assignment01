using E_Commerce.DTO;
using E_Commerce.Interface;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController:Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
      

        [HttpPost]
        public async Task<CustomerDTO> AddCustomer(CustomerDTO customerDTO)
        {
            var response = await _customerService.AddCustomer(customerDTO);
            return response;

        }
        [HttpPost]
        public async Task<CustomerDTO> GetCustomerByUId(string UId)
        {
            var response = await _customerService.GetCustomerByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<List<CustomerDTO>> GetAllCustomer()
        {
            var response = await _customerService.GetAllCustomer();
            return response;
        }

        [HttpPost]
        public async Task<CustomerDTO> UpdateCustomer(CustomerDTO customerDTO)
        {
            var response = await _customerService.UpdateCustomer(customerDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteCustomer(string UId)
        {
            var response = await _customerService.DeleteCustomer(UId);
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


            var customers = new List<CustomerDTO>();
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
                        
                        var customer = new CustomerDTO
                        {
                            UId = GetStringFormCell(worksheet, row, 1),
                            Name = GetStringFormCell(worksheet, row, 2),
                            Email = GetStringFormCell(worksheet, row, 3),
                            Address= GetStringFormCell(worksheet, row, 4),
                            PhoneNumber = GetStringFormCell(worksheet, row, 5),

                        };
                        await AddCustomer(customer);

                        customers.Add(customer);
                    }
                }
            }
            return Ok((customers));
        }


        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var products = await _customerService.GetAllCustomer();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("customers");

                worksheet.Cells[1, 1].Value = "UId";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Address";
                worksheet.Cells[1, 5].Value = "PhoneNumber";
               
                

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
                    worksheet.Cells[i + 2, 3].Value = product.Email;
                    worksheet.Cells[i + 2, 4].Value = product.Address;
                    worksheet.Cells[i + 2, 5].Value = product.PhoneNumber;
                  


                }

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Customer.xlsx";
                return File(stream, "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

            }
        }






    }
}
