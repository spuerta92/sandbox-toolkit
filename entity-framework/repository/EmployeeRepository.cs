using entity_framework.repository.interfaces;
using toolbox.Models;

namespace entity_framework.repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyCompanyContext _db;
        public EmployeeRepository(MyCompanyContext db)
        {
            _db = db;
        }

        public void DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int employeeId)
        {
            return _db.Employees.Single(e => e.EmployeeId.Equals(employeeId));
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _db.Employees;
        }

        public int InsertEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
