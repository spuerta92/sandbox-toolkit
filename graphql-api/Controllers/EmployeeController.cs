using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using toolbox.Enums;
using toolbox.Models;

namespace graphql_api.Controllers
{
    public class EmployeeController : GraphController
    {
        [QueryRoot("Employee")]
        public Employee Employee()
        {
            return new Employee()
            {
                EmployeeId = 1,
                EmployeeName = "Charlie",
                DepartmentId = (byte)Departments.InvestmentResearch,
                RoleId = (byte)Roles.ChiefFinancialOfficer,
                ProjectId = (short)Projects.ExpenseOptimizationInitiative,
                StartDate = DateTime.Now
            };
        }
    }
}
