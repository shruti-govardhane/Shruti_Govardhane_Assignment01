using Employee_Management_System.DTO;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Employee_Management_System.Interface;
using System.Globalization;
namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Import_Export : Controller
    {

        private readonly IEmployeeBasicDetails _employeeBasicDetails;

        public Import_Export(IEmployeeBasicDetails employeeBasicDetails)
        {
            _employeeBasicDetails = employeeBasicDetails;
        }

        [HttpPost]

        public async Task<EmployeeBasicDetailsDTO> AddEmployee(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var response = await _employeeBasicDetails.AddEmployee(employeeBasicDetailsDTO);
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
                   return BadRequest("File is empty ");
            var employees = new List<EmployeeBasicDetailsDTO>();
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
                        DateTime dateOfBirth = DateTime.Parse(GetStringFormCell(worksheet, row, 6));
                        DateTime dateOfJoining = DateTime.Parse(GetStringFormCell(worksheet, row, 7));
                       //Console.WriteLine(dateOfBirth);
                        //Console.WriteLine(dateOfJoining);
                        var employee = new EmployeeBasicDetailsDTO
                       {
                            FirstName = GetStringFormCell(worksheet, row, 1),
                            LastName = GetStringFormCell(worksheet, row, 2),
                            Email = GetStringFormCell(worksheet, row, 3),
                           Mobile = GetStringFormCell(worksheet, row, 4),
                          ReportingManagerName = GetStringFormCell(worksheet, row, 5),
                           DateOfBirth = dateOfBirth,
                          DateOfJoining = dateOfJoining,


                       };
                      await AddEmployee(employee);

                     employees.Add(employee);
                                           }

                   }
               }
               return Ok((employees));

           }
               [HttpGet]
             public async Task<IActionResult> Export()
             {
                 var emoloyees = await _employeeBasicDetails.GetAllEmployee();
                 ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                 using (var package = new ExcelPackage())
                 {
                     var worksheet = package.Workbook.Worksheets.Add("Employees");



                     worksheet.Cells[1, 1].Value = "FirstName";
                     worksheet.Cells[1, 2].Value = "LastName";
                     worksheet.Cells[1, 3].Value = "Email";
                     worksheet.Cells[1, 4].Value = "Mobile";
                     worksheet.Cells[1, 5].Value = "ReportingManagerName";
                     worksheet.Cells[1, 6].Value = "DateOfBirth";
                     worksheet.Cells[1, 7].Value = "DateOfJoining";




                     using (var range = worksheet.Cells[1, 1, 1, 7])
                     {
                         range.Style.Font.Bold = true;
                         range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                         range.Style.Fill.BackgroundColor.SetColor(Color.Blue);

                     }



                     for (int i = 0; i < emoloyees.Count; i++)
                     {
                         var emoloyee = emoloyees[i];

                         worksheet.Cells[i + 2, 1].Value = emoloyee.FirstName;
                         worksheet.Cells[i + 2, 2].Value = emoloyee.LastName;
                         worksheet.Cells[i + 2, 3].Value = emoloyee.Email;
                         worksheet.Cells[i + 2, 4].Value = emoloyee.Mobile;
                         worksheet.Cells[i + 2, 5].Value = emoloyee.ReportingManagerName;
                    worksheet.Cells[i + 2, 6].Value = emoloyee.DateOfBirth;
                    worksheet.Cells[i + 2, 7].Value = emoloyee.DateOfJoining;



                     }

                     var stream = new System.IO.MemoryStream();
                     package.SaveAs(stream);
                     stream.Position = 0;

                     var fileName = "Employee.xlsx";
                     return File(stream, "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                 }
             }






    }
}


