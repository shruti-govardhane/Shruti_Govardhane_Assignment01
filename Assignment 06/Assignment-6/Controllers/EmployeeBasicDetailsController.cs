using Employee_Management_System.DTO;
using Employee_Management_System.Entity;
using Employee_Management_System.Interface;
using Employee_Management_System.Service;
using Employee_Management_System.ServiceFolder;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeBasicDetailsController : Controller
    {

        private readonly IEmployeeBasicDetails  _employeeBasicDetails;

        public EmployeeBasicDetailsController(IEmployeeBasicDetails employeeBasicDetails)
        {
            _employeeBasicDetails = employeeBasicDetails;
        }

        [HttpPost]

        public async Task<EmployeeBasicDetailsDTO> AddEmployee(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var response = await _employeeBasicDetails.AddEmployee(employeeBasicDetailsDTO);
            return response;
        }


     [HttpGet]

        public async Task<List<EmployeeBasicDetailsDTO>> GetAllEmployee()
        {
            var response = await _employeeBasicDetails.GetAllEmployee();
            return response;
        }

       [HttpGet]

        public async Task<EmployeeBasicDetailsDTO> GetEmployeeByUId(string UId)
        {
            var response = await _employeeBasicDetails.GetEmployeeByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<EmployeeBasicDetailsDTO> UpdateEmployee(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var response = await _employeeBasicDetails.UpdateEmployee(employeeBasicDetailsDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteEmployee(string UId)
        {
            var response = await _employeeBasicDetails.DeleteEmployee(UId);
            return response;
        }
        [HttpGet]
        public async Task<List<EmployeeBasicDetailsDTO>> GetEmployeeByRole(string role)
        {
            var response = await _employeeBasicDetails.GetEmployeeByRole(role);
            return response;
        }

        //Pagination
        [HttpPost]
        [ServiceFilter(typeof(BuildEmployeeFilter))]
        public async Task<EmployeeFilter> GetEmployeebypagination(EmployeeFilter employeeFilter)
        {
            var response = await _employeeBasicDetails.GetEmployeebypagination(employeeFilter);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentByMakePostRequest(StudentDTO studentDTO)
        {
            var response = await _employeeBasicDetails.AddStudentByMakePostRequest(studentDTO);
            return Ok(response);

       [HttpGet]
        public async Task<List<StudentDTO>> GetStudentByMakeGetRequest()
        {
           var response = await _employeeBasicDetails.GetStudentByMakeGetRequest();
            return response;
        }



    }
}
