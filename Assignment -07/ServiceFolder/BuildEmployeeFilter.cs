

using Employee_Management_System.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Employee_Management_System.ServiceFolder
{
    public class BuildEmployeeFilter:IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeFilter);
            if(param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }
            EmployeeFilter employeeFilter=(EmployeeFilter)param.Value;
            var statusFilter = employeeFilter.filters.Find(a=>a.FieldName == "status");
            if(statusFilter != null)
            {
                statusFilter = new Filter();
                statusFilter.FieldValue = "Active";
                statusFilter.FieldName= "status";
                employeeFilter.filters.Add(statusFilter);
            }
            employeeFilter.filters.RemoveAll(a=>string.IsNullOrEmpty(a.FieldName));

            var result = await next();
        }
    }
}
