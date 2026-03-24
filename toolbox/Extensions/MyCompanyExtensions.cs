using toolbox.Models;
using toolbox.Records;

namespace toolbox.Extensions
{
    public static class MyCompanyExtensions
    {
        public static EmployeeDto AsEmployeeDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.EmployeeId,
                Name = employee.EmployeeName,
                RoleId = employee.RoleId,
                DepartmentId = employee.DepartmentId,
                //ProjectId = employee.ProjectId,
                StartDate = employee.StartDate,
                TerminationDate = employee.TerminationDate
            };
        }

        public static DepartmentDto AsDepartmentDto(this Department department)
        {
            return new DepartmentDto
            {
                Id = department.DeparmentId,
                Name = department.DepartmentName
            };
        }

        public static RoleDto AsRoleDto(this Role role)
        {
            return new RoleDto
            {
                Id = role.RoleId,
                Name = role.RoleName,
                Description = role.Description
            };
        }

        public static ProjectDto AsProjectDto(this Project project)
        {
            return new ProjectDto
            {
                Id = project.ProjectId,
                Name = project.ProjectName,
                BudgetAmount = project.BudgetAmount,
                Description = project.Description, 
                StartTime = project.StartTime,
                EndTime = project.EndTime
            };
        }
    }
}
