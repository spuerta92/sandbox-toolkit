using toolbox.Models;

namespace entity_framework.repository.interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int employeeId);
        void UpdateEmployee(Employee employee);
        int InsertEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
