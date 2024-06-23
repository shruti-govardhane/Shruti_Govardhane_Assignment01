using Employee_Management_System.Common;
using Employee_Management_System.Entity;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using Container = Microsoft.Azure.Cosmos.Container;

namespace Employee_Management_System.CosmosDB
{
    public class CosmosDBService : ICosmosDBService
    {
        public readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        public CosmosDBService()

        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndPoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);
        }
        public async Task<EmployeeBasicDetailsEntity> AddEmployee(EmployeeBasicDetailsEntity employee)
        {
            var response = await _container.CreateItemAsync(employee);
            return response;
        }
      public async Task<List<EmployeeBasicDetailsEntity>> GetAllEmployee()
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true).Where(a => a.Active == true && a.Archived == false
            && a.DocumentType == Credentials.EmployeeDocumentType).ToList();
            return response;
        }
        public async Task<EmployeeBasicDetailsEntity> GetEmployeeByUId(string uId)
        {
            var employee = _container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true).Where(a => a.UId == uId && !a.Archived
            && a.Active && a.DocumentType == Credentials.EmployeeDocumentType).FirstOrDefault();
            return employee;
        }

        public async Task ReplaceAsync(dynamic entity)
        {
            var response = await _container.ReplaceItemAsync(entity, entity.Id);
        }


        public async Task<EmployeeAdditionalDetailsEntity> Add_AdditionalData(EmployeeAdditionalDetailsEntity employee)
        {
            var response = await _container.CreateItemAsync(employee);
            return response;
        }
        public async Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeeAdditionalData()
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true).Where(a => a.Active == true && a.Archived == false
            && a.DocumentType == Credentials.EmployeeDocumentType).ToList();
            return response;
        }


       public async Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDataByUId(string uId)
        {
            var employee = _container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true).Where(a => a.UId == uId && !a.Archived
            && a.Active && a.DocumentType == Credentials.EmployeeDocumentType).FirstOrDefault();
            return employee;
        }
    }
}
