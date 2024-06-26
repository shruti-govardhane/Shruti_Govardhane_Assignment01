using Employee_Management_System.Common;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.DTO;
using Employee_Management_System.Entity;
using Employee_Management_System.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Employee_Management_System.Service
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetails
    {
        public readonly ICosmosDBService _cosmosDBService;

        public EmployeeBasicDetailsService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        //ADD

        public async Task<EmployeeBasicDetailsDTO> AddEmployee(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            EmployeeBasicDetailsEntity employee= new EmployeeBasicDetailsEntity();
            employee.Salutory = employeeBasicDetailsDTO.Salutory;
            employee.FirstName = employeeBasicDetailsDTO.FirstName;
            employee.MiddleName = employeeBasicDetailsDTO.MiddleName;
            employee.LastName = employeeBasicDetailsDTO.LastName;
            employee.NickName = employeeBasicDetailsDTO.NickName;
            employee.Email = employeeBasicDetailsDTO.Email;
            employee.Mobile = employeeBasicDetailsDTO.Mobile;
            employee.EmployeeId = employeeBasicDetailsDTO.EmployeeId;
            employee.Role = employeeBasicDetailsDTO.Role;
            employee.ReportingManagerUId = Guid.NewGuid().ToString();
            employee.ReportingManagerName = employeeBasicDetailsDTO.ReportingManagerName;
            employee.Address = employeeBasicDetailsDTO.Address;
            employee.Status = employeeBasicDetailsDTO.Status;

           

            employee.Intialize(true, Credentials.EmployeeDocumentType, "Shruti", "Shruti");


            var response = await _cosmosDBService.AddEmployee(employee);

            var responseModel = new EmployeeBasicDetailsDTO();
            responseModel.UId = response.UId;
            responseModel.Salutory = response.Salutory;
            responseModel.FirstName = response.FirstName;
            responseModel.MiddleName = response.MiddleName;
            responseModel.LastName = response.LastName;
            responseModel.NickName = response.NickName;
            responseModel.Email = response.Email;
            responseModel.Mobile = response.Mobile;
            responseModel.EmployeeId= response.Id;
            responseModel.Role = response.Role;
            responseModel.ReportingManagerUId = response.ReportingManagerUId;
            responseModel.ReportingManagerName = response.ReportingManagerName;
            responseModel.Address = response.Address;
            responseModel.Status=response.Status;
          


            return responseModel;

        }

        //GetAll

      public async Task<List<EmployeeBasicDetailsDTO>> GetAllEmployee()
        {
            var employees = await _cosmosDBService.GetAllEmployee();

            var employeeBasicDetailsDTOs = new List<EmployeeBasicDetailsDTO>();
            foreach (var employee in employees)
            {
                var employeeBasicDetailsDTO = new EmployeeBasicDetailsDTO();
                employeeBasicDetailsDTO.UId = employee.UId;
                employeeBasicDetailsDTO.Salutory = employee.Salutory;
                employeeBasicDetailsDTO.FirstName = employee.FirstName;
                employeeBasicDetailsDTO.MiddleName = employee.MiddleName;
                employeeBasicDetailsDTO.LastName = employee.LastName;
                employeeBasicDetailsDTO.NickName = employee.NickName;
                employeeBasicDetailsDTO.Email = employee.Email;
                employeeBasicDetailsDTO.Mobile = employee.Mobile;
                employeeBasicDetailsDTO.EmployeeId = employee.Id;
                employeeBasicDetailsDTO.Role = employee.Role;
                employeeBasicDetailsDTO.ReportingManagerUId = employee.ReportingManagerUId;
                employeeBasicDetailsDTO.ReportingManagerName = employee.ReportingManagerName;
                employeeBasicDetailsDTO.Address = employee.Address;
                employeeBasicDetailsDTO.Status = employee.Status;
                employeeBasicDetailsDTOs.Add(employeeBasicDetailsDTO);


            }
            return employeeBasicDetailsDTOs;
        }

        //Getbyuid
        public async Task<EmployeeBasicDetailsDTO> GetEmployeeByUId(string UId)
        {
            var response = await _cosmosDBService.GetEmployeeByUId(UId);

            var employeeBasicDetailsDTO = new EmployeeBasicDetailsDTO();
            employeeBasicDetailsDTO.UId=response.UId;
            employeeBasicDetailsDTO.Salutory = response.Salutory;
            employeeBasicDetailsDTO.FirstName = response.FirstName;
            employeeBasicDetailsDTO.MiddleName = response.MiddleName;
            employeeBasicDetailsDTO.LastName = response.LastName;
            employeeBasicDetailsDTO.NickName = response.NickName;
            employeeBasicDetailsDTO.Email = response.Email;
            employeeBasicDetailsDTO.Mobile = response.Mobile;
            employeeBasicDetailsDTO.EmployeeId = response.Id;
            employeeBasicDetailsDTO.Role = response.Role;
            employeeBasicDetailsDTO.ReportingManagerUId = response.ReportingManagerUId;
            employeeBasicDetailsDTO.ReportingManagerName = response.ReportingManagerName;
            employeeBasicDetailsDTO.Address = response.Address;
            employeeBasicDetailsDTO.Status = response.Status;


            return employeeBasicDetailsDTO;
        }

        //update

        public async Task<EmployeeBasicDetailsDTO> UpdateEmployee(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeByUId(employeeBasicDetailsDTO.UId);
            existingEmployee.Active = false;
            existingEmployee.Archived = true;
            await _cosmosDBService.ReplaceAsync(existingEmployee);

            existingEmployee.Intialize(false, Credentials.EmployeeDocumentType, "Shruti", "Shruti");



            existingEmployee.Salutory = employeeBasicDetailsDTO.Salutory;
            existingEmployee.FirstName = employeeBasicDetailsDTO.FirstName;
            existingEmployee.MiddleName = employeeBasicDetailsDTO.MiddleName;
            existingEmployee.LastName = employeeBasicDetailsDTO.LastName;
            existingEmployee.NickName = employeeBasicDetailsDTO.NickName;
            existingEmployee.Email = employeeBasicDetailsDTO.Email;
            existingEmployee.Mobile = employeeBasicDetailsDTO.Mobile;
            existingEmployee.EmployeeId = employeeBasicDetailsDTO.EmployeeId;
            existingEmployee.Role = employeeBasicDetailsDTO.Role;
            existingEmployee.ReportingManagerUId = employeeBasicDetailsDTO.ReportingManagerUId;
            existingEmployee.ReportingManagerName = employeeBasicDetailsDTO.ReportingManagerName;
            existingEmployee.Address = employeeBasicDetailsDTO.Address;
            

            var response = await _cosmosDBService.AddEmployee(existingEmployee);

            var responseModel = new EmployeeBasicDetailsDTO
            {
                UId = response.UId,
               Salutory = response.Salutory,
            FirstName = response.FirstName,
            MiddleName = response.MiddleName,
            LastName = response.LastName,
            NickName = response.NickName,
            Email = response.Email,
            Mobile = response.Mobile,
           EmployeeId = response.Id,
            Role = response.Role,
            ReportingManagerUId = response.ReportingManagerUId,
            ReportingManagerName = response.ReportingManagerName,
            Address = response.Address,


        };
            return responseModel;


        }
        //delete
        public async Task<string> DeleteEmployee(string uId)
        {
      
            var employee = await _cosmosDBService.GetEmployeeByUId(uId);
            employee.Active = false;
            employee.Archived = true;
            await _cosmosDBService.ReplaceAsync(employee);

            employee.Intialize(false, Credentials.EmployeeDocumentType, "Shruti", "Shruti");
            employee.Archived = true;

            

            var response = await _cosmosDBService.AddEmployee(employee);

            return "Record Deleted Successfully";

        }
        //find 

        public async Task<List<EmployeeBasicDetailsDTO>> GetEmployeeByRole(string role)
        {
            var allStudents = await GetAllEmployee();

            var filteredList = allStudents.FindAll(a => a.Role == role);

            return filteredList;
        }


        //Filter
        public async Task<EmployeeFilter> GetEmployeebypagination(EmployeeFilter employeeFilter)
        {
            EmployeeFilter response =new EmployeeFilter();

            var check = employeeFilter.filters.Any(a => a.FieldName == "status");
            var status = "";
            if(check)
            {
                status = employeeFilter.filters.Find(a => a.FieldName == "status").FieldValue;
            }
             var employees= await GetAllEmployee();
            var filterRecord=employees.FindAll(a=>a.Status==status);
            response.totalCount = employees.Count;
            response.page=employeeFilter.page;
            response.pageSize=employeeFilter.pageSize;




            var s = employeeFilter.pageSize * (employeeFilter.page - 1);

            employees = employees.Skip(s).Take(employeeFilter.pageSize).ToList();

            foreach(var employee in employees)
            {
                response.employeeBasicDetails.Add(employee);
            }
            return response;
        }

        public async Task<IActionResult> AddStudentByMakePostRequest(StudentDTO studentDTO)
        {
            var serializedObj = JsonConvert.SerializeObject(studentDTO);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.StudentUrl, Credentials.AddStudentEndpoint, serializedObj);
            var studentdto = JsonConvert.DeserializeObject<StudentDTO>(requestObj);
            return new JsonResult(studentdto);
        }public async Task<List<StudentDTO>> GetStudentByMakeGetRequest()
        {
            var request = await HttpClientHelper.MakeGetRequest(Credentials.StudentUrl, Credentials.GetStudentEndPoint);
            return JsonConvert.DeserializeObject<List<StudentDTO>>(request);
        }

    }
}
